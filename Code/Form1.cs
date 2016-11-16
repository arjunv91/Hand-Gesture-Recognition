using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Speech;
using System.Speech.Synthesis;
using Emgu.CV.VideoSurveillance;
using HandGestureRecognition.SkinDetector;

namespace Project_Phase1
{
    public partial class Form1 : Form
    {
        Capture capwebcam = null;
        bool bmcapprocess = false;
        Image<Bgr, Byte> imgOriginal;
        Image<Gray, Byte> imgProcessed;
        Image<Gray, Byte> GrayDifference;
        Image<Bgr, Byte> currentFrame;
        Image<Bgr, Byte> prevFrame;
        Image<Bgr, Byte> Difference;
        SpeechSynthesizer ss = new SpeechSynthesizer();
     //   IBGFGDetector<Bgr> _forgroundDetector;
        Seq<Point> hull;
        Seq<Point> filteredHull;
        Seq<MCvConvexityDefect> defects;
        MCvConvexityDefect[] defectArray;
        String gesture;
        Rectangle handRect;
       MCvBox2D box;
       Ellipse ellip;

       double fingLen;
       double dist;
       int superflag;

       BackgroundSubtractorMOG2 bgs;
        AdaptiveSkinDetector detector;
       Hsv hsv_min;
        Hsv hsv_max;
       Ycc YCrCb_min;
        Ycc YCrCb_max;
        IColorSkinDetector skinDetector;
      PointF cogPt;
      private int contourAxisAngle;
      MCvMoments mv;
        public Form1()
        {
            InitializeComponent();
            detector = new AdaptiveSkinDetector(1, AdaptiveSkinDetector.MorphingMethod.NONE);
          hsv_min = new Hsv(0, 45, 0);
            hsv_max = new Hsv(20, 255, 255);
           YCrCb_min = new Ycc(0, 131, 80);
           YCrCb_max = new Ycc(255, 185, 135);
           float flot = 15;
         bgs =  new BackgroundSubtractorMOG2(30, flot , false);
            mv = new MCvMoments();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                capwebcam = new Capture();
            }
            catch (NullReferenceException except)
            {
                tbGesture.Text = except.Message;
                return;
            }

            Application.Idle += processFrameAndUpdateGUI;

            bmcapprocess = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (capwebcam != null)
                capwebcam.Dispose();

        }


        void processFrameAndUpdateGUI(object sender, EventArgs e)
        {
         /*   if (imgOriginal == null)
            {
                imgOriginal = capwebcam.RetrieveBgrFrame(); //we could use RetrieveGrayFrame if we didn't care about displaying colour image
                prevFrame = imgOriginal.Copy();
            }
            */

          imgOriginal = capwebcam.RetrieveBgrFrame();
          currentFrame = capwebcam.QueryFrame();
            if (imgOriginal == null) return;

           

          //  imgProcessed = /*here*/;
          //  imgProcessed = imgProcessed.SmoothGaussian(9);

         /*  if (_forgroundDetector == null)
            {
               //_forgroundDetector = new BGCodeBookModel<Bgr>();
               _forgroundDetector = new FGDetector<Bgr>(Emgu.CV.CvEnum.FORGROUND_DETECTOR_TYPE.FGD);
               //_forgroundDetector = new BGStatModel<Bgr>(image, Emgu.CV.CvEnum.BG_STAT_TYPE.FGD_STAT_MODEL);
            }
*/
     //      _forgroundDetector.Update(imgOriginal);

     

            //update the motion history

    //        imgProcessed = _forgroundDetector.ForegroundMask;
         //   imgProcessed = imgProcessed.SmoothGaussian(9);
        
            
        /*    bgs.Update(imgOriginal);
            imgProcessed = bgs.ForegroundMask;
          */ 


           skinDetector = new YCrCbSkinDetector();

            Image<Gray, Byte> skin = skinDetector.DetectSkin(currentFrame, YCrCb_min, YCrCb_max);

           //imgProcessed.AddWeighted(skin, 1.0, 1.0, 1.0);
          
            //imgProcessed.Or(skin);

   /*         Difference = prevFrame.AbsDiff(imgOriginal);          
            Difference = Difference.ThresholdBinary(new Bgr(60, 60, 60), new Bgr(255, 255, 255));           
            GrayDifference = Difference.Convert<Gray, Byte>();
            imgProcessed.AddWeighted(GrayDifference, 1.0, 1.0, 1.0);
     
    */
          imgProcessed = skin.SmoothGaussian(9);
        

   
   // imgProcessed = imgProcessed.Erode(5);

          /*  foreach (CircleF circle in circles)
            {
                if (tbGesture.Text != "") tbGesture.AppendText(Environment.NewLine);
                tbGesture.AppendText("Hull Position = x - " + circle.Center.X.ToString().PadLeft(4) +
                                        ", y - " + circle.Center.Y.ToString().PadLeft(4) +
                                        ",radius - " + circle.Radius.ToString("###.000").PadLeft(7));
                tbGesture.ScrollToCaret();
                CvInvoke.cvCircle(imgOriginal,
                                  new Point((int)circle.Center.X, (int)circle.Center.Y),
                                   3,
                                   new MCvScalar(0, 255, 0),
                                   -1,
                                   LINE_TYPE.CV_AA,
                                   0);

                imgOriginal.Draw(circle,
                                new Bgr(Color.Red),
                                3);
                ss.Speak("Found your red circle!");
            }*/

          
          
            ExtractContourAndHull(imgProcessed);
            if(defects != null)
                DrawAndComputeFingersNum();

            ibProcessed.Image = imgProcessed;
            ibOriginal.Image = currentFrame;

        }
     
        /// /////////// paste here

        private void ExtractContourAndHull(Image<Gray, byte> skin)
        {
            using (MemStorage storage = new MemStorage())
            {

                Contour<Point> contours = skin.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST, storage);
                Contour<Point> biggestContour = null;

                Double Result1 = 0;
                Double Result2 = 0;
                while (contours != null)
                {
                    Result1 = contours.Area;
                    if (Result1 > Result2)
                    {
                        Result2 = Result1;
                        biggestContour = contours;
                    }
                    contours = contours.HNext;
                }

                if (biggestContour != null)
                {
                    //currentFrame.Draw(biggestContour, new Bgr(Color.DarkViolet), 2);
                    Contour<Point> currentContour = biggestContour.ApproxPoly(biggestContour.Perimeter * 0.0025, storage);
                    currentFrame.Draw(currentContour, new Bgr(Color.LimeGreen), 2);
                    biggestContour = currentContour;


                    hull = biggestContour.GetConvexHull(Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);
                    box = biggestContour.GetMinAreaRect();
                    PointF[] points = box.GetVertices();
                    
                    
                   
                    mv = biggestContour.GetMoments();
                    CvInvoke.cvMoments(biggestContour,ref mv, 1);
                    double m00 = CvInvoke.cvGetSpatialMoment(ref mv, 0, 0) ;
                    double m10 = CvInvoke.cvGetSpatialMoment(ref mv, 1, 0) ;
                    double m01 = CvInvoke.cvGetSpatialMoment(ref mv, 0, 1) ;
                   
                    if (m00 != 0) { // calculate center
                    int xCenter = (int) Math.Round(m10/m00)*2;  //scale = 2
                    int yCenter = (int) Math.Round(m01/m00)*2;
                    cogPt.X =xCenter;
                    cogPt.Y =yCenter; 
                    }

                    double m11 = CvInvoke.cvGetCentralMoment(ref mv, 1, 1);
                    double m20 = CvInvoke.cvGetCentralMoment(ref mv, 2, 0);
                    double m02 = CvInvoke.cvGetCentralMoment(ref mv, 0, 2);
                    contourAxisAngle = calculateTilt(m11, m20, m02);
                    tbHandAngle.Text = contourAxisAngle.ToString();
          
           // deal with hand contour pointing downwards
/* uses fingertips inf
ormation generated on the last update of
the hand, so will be out
-
of
-
date */
                  
            /*        
                    if (fingerTips.size() > 0) {
                    int yTotal = 0;
                    for(Point pt : fingerTips)
                    yTotal += pt.y;
                    int avgYFinger = yTotal/fingerTips.size();
                    if (avgYFinger > cogPt.y) // fingers below COG
                    contourAxisAngle += 180;
                    }
                    contourAxisAngle = 180-contourAxisAngle;



/* this makes the angle relative to a positive y
-
axis that
runs up the screen */


                    //handRect = box.MinAreaRect();
                    //currentFrame.Draw(handRect, new Bgr(200, 0, 0), 1);

                    Point[] ps = new Point[points.Length];
                    for (int i = 0; i < points.Length; i++)
                        ps[i] = new Point((int)points[i].X, (int)points[i].Y);

                    currentFrame.DrawPolyline(hull.ToArray(), true, new Bgr(200, 125, 75), 2);
                    currentFrame.Draw(new CircleF(new PointF(box.center.X, box.center.Y), 3), new Bgr(200, 125, 75), 2);

                    //ellip.MCvBox2D= CvInvoke.cvFitEllipse2(biggestContour.Ptr);
                    //currentFrame.Draw(new Ellipse(ellip.MCvBox2D), new Bgr(Color.LavenderBlush), 3);

                    PointF center;
                    float radius;
                    //CvInvoke.cvMinEnclosingCircle(biggestContour.Ptr, out  center, out  radius);
                    //currentFrame.Draw(new CircleF(center, radius), new Bgr(Color.Gold), 2);

                    //currentFrame.Draw(new CircleF(new PointF(ellip.MCvBox2D.center.X, ellip.MCvBox2D.center.Y), 3), new Bgr(100, 25, 55), 2);
                    //currentFrame.Draw(ellip, new Bgr(Color.DeepPink), 2);

                    //CvInvoke.cvEllipse(currentFrame, new Point((int)ellip.MCvBox2D.center.X, (int)ellip.MCvBox2D.center.Y), new System.Drawing.Size((int)ellip.MCvBox2D.size.Width, (int)ellip.MCvBox2D.size.Height), ellip.MCvBox2D.angle, 0, 360, new MCvScalar(120, 233, 88), 1, Emgu.CV.CvEnum.LINE_TYPE.EIGHT_CONNECTED, 0);
                    //currentFrame.Draw(new Ellipse((box.center.X, box.centernew PointF.Y), new SizeF(box.size.Height, box.size.Width), box.angle), new Bgr(0, 0, 0), 2);


                    filteredHull = new Seq<Point>(storage);
                    for (int i = 0; i < hull.Total; i++)
                    {
                        if (Math.Sqrt(Math.Pow(hull[i].X - hull[i + 1].X, 2) + Math.Pow(hull[i].Y - hull[i + 1].Y, 2)) > box.size.Width / 10)
                        {
                            filteredHull.Push(hull[i]);
                        }
                    }

                    defects = biggestContour.GetConvexityDefacts(storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE);

                    defectArray = defects.ToArray();
                }
            }
        }

        private int calculateTilt(double m11, double m20, double m02)
        {
            double diff = m20-m02;
            if (diff == 0) {
                if (m11 == 0)
                    return 0;
                else if (m11 > 0)
                    return 45;
                else // m11 < 0
                    return -45;
            }
            double theta = 0.5 * Math.Atan2(2*m11, diff);
            int tilt = (int) Math.Round( 57.2957795*theta);
            if ((diff > 0) && (m11 == 0))
                    return 0;
            else if ((diff < 0) && (m11 == 0))
                    return -90;
            else if ((diff > 0) && (m11 > 0)) // 0 to 45 degrees
                    return tilt;
            else if ((diff > 0) && (m11 < 0)) //-45 to 0
                return (180 + tilt); // change to counter-clockwise angle
            else if ((diff < 0) && (m11 > 0)) // 45 to 90
                    return tilt;
            else if ((diff < 0) && (m11 < 0)) //-90 to-45
                    return (180 + tilt); // change tocounter-clockwise angle
            tbGesture.Text= "Error in moments for tilt angle";
                return 0;
                } // end of calculateTilt()


        private void DrawAndComputeFingersNum()
        {
            //int fingerNum = 0;
            int fingerNum = 0;
            fingLen = 0;
       

            tbFingerPosition.Text = "";
            #region hull drawing
            //for (int i = 0; i < filteredHull.Total; i++)
            //{
            //    PointF hullPoint = new PointF((float)filteredHull[i].X,
            //                                  (float)filteredHull[i].Y);
            //    CircleF hullCircle = new CircleF(hullPoint, 4);
            //    currentFrame.Draw(hullCircle, new Bgr(Color.Aquamarine), 2);
            //}
            #endregion

            #region defects drawing
            for (int i = 0; i < defects.Total; i++)
            {
                PointF startPoint = new PointF((float)defectArray[i].StartPoint.X,
                                                (float)defectArray[i].StartPoint.Y);

                PointF depthPoint = new PointF((float)defectArray[i].DepthPoint.X,
                                                (float)defectArray[i].DepthPoint.Y);

                PointF endPoint = new PointF((float)defectArray[i].EndPoint.X,
                                                (float)defectArray[i].EndPoint.Y);

                

                LineSegment2D startDepthLine = new LineSegment2D(defectArray[i].StartPoint, defectArray[i].DepthPoint);

                LineSegment2D depthEndLine = new LineSegment2D(defectArray[i].DepthPoint, defectArray[i].EndPoint);

                CircleF startCircle = new CircleF(startPoint, 5f);

                CircleF depthCircle = new CircleF(depthPoint, 5f);

                CircleF endCircle = new CircleF(endPoint, 5f);

               

                //Custom heuristic based on some experiment, double check it before use
                if (/*(startCircle.Center.Y < box.center.Y || depthCircle.Center.Y < box.center.Y) &&*/ (startCircle.Center.Y < depthCircle.Center.Y) && (Math.Sqrt(Math.Pow(startCircle.Center.X - depthCircle.Center.X, 2) + Math.Pow(startCircle.Center.Y - depthCircle.Center.Y, 2)) > box.size.Height / 6.5))
                {
                    fingerNum++;
                    currentFrame.Draw(startDepthLine, new Bgr(Color.Green), 2);
                    if (fingLen < Math.Sqrt(Math.Pow(startCircle.Center.X - depthCircle.Center.X, 2) + Math.Pow(startCircle.Center.Y - depthCircle.Center.Y, 2)))
                        fingLen = Math.Sqrt(Math.Pow(startCircle.Center.X - depthCircle.Center.X, 2) + Math.Pow(startCircle.Center.Y - depthCircle.Center.Y, 2));
                    //currentFrame.Draw(depthEndLine, new Bgr(Color.Magenta), 2);
                }

               

                currentFrame.Draw(startCircle, new Bgr(Color.Red), 2);
                currentFrame.Draw(depthCircle, new Bgr(Color.Yellow), 5);
                //currentFrame.Draw(endCircle, new Bgr(Color.DarkBlue), 4);
            }
            #endregion
            /*
            MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_DUPLEX, 5d, 5d);
            currentFrame.Draw(fingerNum.ToString(), ref font, new Point(50, 150), new Bgr(Color.White));
            */

            //print finger tip position

            for(int i=0; i< defects.Total; i++) 
            tbFingerPosition.Text += "(" + defectArray[i].StartPoint.X.ToString() + " , " + defectArray[i].StartPoint.Y.ToString() + ")\n";


/*
            CircleF[] circles = imgProcessed.HoughCircles(new Gray(100),
                                                        new Gray(50),
                                                        2,
                                                        imgProcessed.Height / 4,
                                                        10,
                                                        400)[0];

            superflag = 0;
            if (circles.Length > 1)
            {
                dist = Math.Sqrt(Math.Pow(circles[0].Center.X - circles[1].Center.X, 2) + Math.Pow(circles[0].Center.Y - circles[1].Center.Y, 2));
                if (dist < 5)
                    superflag = 1;
            }


*/
            
            if (fingLen < 130)
            {
                fingerNum = 0;
            }

            tbNoFinger.Text = fingerNum.ToString();
            gestureRecog(fingerNum);

        }


        private void gestureRecog(int fingerNum)
        {   
          /*  if (superflag ==1)
                gesture = tbGesture.Text = "Soooper!";
            */
            if(fingerNum == 0)
                gesture = tbGesture.Text = "Fist!";
            
            else if (fingerNum == 1 && contourAxisAngle > 130 && contourAxisAngle < 165)
                gesture = tbGesture.Text = "Thumbs up!";
            else if (fingerNum == 2 && contourAxisAngle > 90 && contourAxisAngle < 125)
                gesture = tbGesture.Text = "Victory!";
            else if (fingerNum == 2 && contourAxisAngle > 130 && contourAxisAngle < 165)
                gesture = tbGesture.Text = "Go Left!";
            else if (fingerNum == 2 && contourAxisAngle > 60 && contourAxisAngle < 90)
                gesture = tbGesture.Text = "Go Right!";
            else if (fingerNum == 1 && contourAxisAngle > 25 && contourAxisAngle < 40)
                gesture = tbGesture.Text = "Thumbs Down!";
            else if (fingerNum == 5)
                gesture = tbGesture.Text = "Open palm!";
            else if (fingerNum == 4)
                gesture = tbGesture.Text = "Four fingers!";
            else if (fingerNum == 3)
                gesture = tbGesture.Text = "Three fingers!";
            else if (fingerNum == 1)
                gesture = tbGesture.Text = "Index finger!";
            else
                gesture = tbGesture.Text = "Unrecognized!";
        }



        ////////////////////
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (bmcapprocess == true)
            {
                Application.Idle -= processFrameAndUpdateGUI;
                bmcapprocess = false;
                btnPause.Text = "resume";
            }
            else
            {
                Application.Idle += processFrameAndUpdateGUI;
                bmcapprocess = true;
                btnPause.Text = "pause";
            }

        }

        private void btnSpeakGesture_Click(object sender, EventArgs e)
        {
            ss.Speak(gesture);
        }
    }
}
