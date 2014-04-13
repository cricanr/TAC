using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TrainingAtentional.Logs;
using System.Configuration;

namespace TrainingAtentional
{
    public partial class DotProbe : BasePage
    {
        #region Properties

        const int CONST_repeatPhotoNr = 3;//
        const int CONST_photoNr = 24; //72;
        const string CONST_congruent = "CGR";
        const string CONST_right = "R";
        const int CONST_TrialNr = 72;//144bun
        const int CONST_TestNr = 4; // primele 4 sunt de test

        bool IsTestTrial
        {
            get { return CurrentTestTrail <= CONST_TestNr; }
        }

        Dictionary<int, string> MapAllPhoto
        {
            get { return (Dictionary<int, string>)ViewState["MapAllPhoto"]; }
            set { ViewState["MapAllPhoto"] = value; }
        }

        List<int> PhotoIDSequence
        {
            get { return (List<int>)ViewState["HPhotoIDSequence"]; }
            set { ViewState["HPhotoIDSequence"] = value; }
        }

        int CurrentIndex
        {
            get { return (int)ViewState["CurrentIndex"]; }
            set { ViewState["CurrentIndex"] = value; }
        }

        int CurrentTestTrail
        {
            get { return (int)ViewState["CurrentTestTrail"]; }
            set { ViewState["CurrentTestTrail"] = value; }
        }

        int CurrentPosition
        {
            get { return (int)ViewState["CurrentPosition"]; }
            set { ViewState["CurrentPosition"] = value; }
        }

        HtmlGenericControl Body
        {
            get { return ((this.Page.Master) as Site).FindControl("body") as HtmlGenericControl; }
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //int x = 10;
                    //int y = 0;
                    //int z = x / y;

                    divInfo.Style["display"] = "";

                    MapAllPhoto = new Dictionary<int, string>();
                    FillMapAllPhoto();

                    string s="MapAllPhoto:";

                    foreach (var item in MapAllPhoto)
                    {
                        s += item.Key + " @ " + item.Value + Environment.NewLine;
                    }
                    MyLogs.WriteError(s);


                    //int[] a = new int[6] { 0, 0, 0, 0, 0, 0 };


                    //foreach (int key in MapAllPhoto.Keys)
                    //{
                    //    if (MapAllPhoto[key].Contains(".CGR.L.")) a[0]++;
                    //    if (MapAllPhoto[key].Contains(".CGR.R.")) a[1]++;
                    //    if (MapAllPhoto[key].Contains(".ICGR.L.")) a[2]++;
                    //    if (MapAllPhoto[key].Contains(".ICGR.R.")) a[3]++;

                    //}

                    //Body.Attributes.Add("onkeypress", "OnBodyKeyPressed();");
                    literalDocumentKeyPressed.Text = "<script type='text/javascript'> document.onkeypress = OnDocumentStartTrialKeyPressed; </script> ";

                    #region Mapari

                    //Dictionary<int, string> MapInHAPhoto = new Dictionary<int, string>();
                    //Dictionary<int, string> MapInNAPhoto = new Dictionary<int, string>();
                    //Dictionary<int, string> MapInNHPhoto = new Dictionary<int, string>();
                    //Dictionary<int, string> MapOutHAPhoto = new Dictionary<int, string>();
                    //Dictionary<int, string> MapOutNAPhoto = new Dictionary<int, string>();
                    //Dictionary<int, string> MapOutNHPhoto = new Dictionary<int, string>();

                    //MethodPool.FillFileMap(MapInHAPhoto, PhotoFolderType.DotProbe_In_HappyAngry);
                    //MethodPool.FillFileMap(MapInNAPhoto, PhotoFolderType.DotProbe_In_NeutruAngry);
                    //MethodPool.FillFileMap(MapInNHPhoto, PhotoFolderType.DotProbe_In_NeutruHappy);
                    //MethodPool.FillFileMap(MapInHAPhoto, PhotoFolderType.DotProbe_In_HappyAngry);
                    //MethodPool.FillFileMap(MapInHAPhoto, PhotoFolderType.DotProbe_In_NeutruAngry);
                    //MethodPool.FillFileMap(MapInNHPhoto, PhotoFolderType.DotProbe_In_NeutruHappy);

                    #endregion

                    PhotoIDSequence = GenerateSequence();
                    //string s = "";
                    //foreach (var item in PhotoIDSequence)
                    //{
                    //    s += item + ",";
                    //}

                    //MyLogs.WriteError("secventa initiala: "+ s);
                    
                    //CurrentIndex = 0;
                    CurrentTestTrail = 1;

                    SetCurrentPositionAndImageUrl();
                }

                else
                {
                    divInfo.Style["display"] = "none";

                    //Body.Attributes.Remove("onkeypress");
                    literalDocumentKeyPressed.Text = "<script type='text/javascript'> document.onkeypress = ''; </script> ";

                    string eventTarget = Request.Params.Get("__EVENTTARGET");
                    if (eventTarget.Contains("keyPressed"))
                    {
                        #region Insert in DB

                        string keyPressed = eventTarget.Split('|')[1];
                        int userID = ExtraMySession.UserID;
                        DateTime dtStart = MethodPool.GetDateFromJS(hStart.Value);
                        DateTime dtStop = MethodPool.GetDateFromJS(hStop.Value);

                        string imageInfo = MapAllPhoto[CurrentPosition];
                        string photoName = GetPhotoName(imageInfo);
                        Gender gender = new Gender();
                        Emotion emotion = new Emotion();
                        int code = 0;
                        bool isCongruent;
                        PhotoOrientation photoOrientation = new PhotoOrientation();
                        TargetType targetType = GetTargetType(img3.ImageUrl);//atentie , ar trebui pastrat doar numele imaginii



                        GetPhotoIndicators(photoName, out code, out emotion, out gender, out isCongruent, out photoOrientation);

                        string wrongKeyPressed = hWrongKeyPressed.Value;
                        DateTime dtStopWrongKeyPressed = new DateTime();
                        if (!string.IsNullOrEmpty(wrongKeyPressed))
                        {
                            dtStopWrongKeyPressed = MethodPool.GetDateFromJS(hStopWrongKeyPressed.Value);
                        }

                        int stage = ExtraMySession.Stage;

                        bool result = DBPool.InsertDotTrial(userID, Session.SessionID, dtStart, dtStop, photoName, code, (int)gender, (int)emotion, (int)photoOrientation, (bool)isCongruent, (int)targetType, IsTestTrial, keyPressed, dtStopWrongKeyPressed, wrongKeyPressed, stage);
                        if (!result)
                        {
                            throw new ApplicationException("eroare la inserare trial dot");
                        }
                        #endregion

                        hWrongKeyPressed.Value = "";
                        hStopWrongKeyPressed.Value = "";

                        #region Prepare next Step

                        #region Cazuri test trail

                        if (CurrentTestTrail < CONST_TestNr)
                        {
                            CurrentTestTrail++;
                            SetCurrentPositionAndImageUrl();
                            int crossDuration = GetRandomCrossDuration();
                            literal.Text = string.Format("<script type='text/javascript'> Step1({0},500); </script> ", crossDuration);
                        }
                        else if (CurrentTestTrail == CONST_TestNr)//s-a salvat in BD si ultimul test
                        {
                            CurrentTestTrail++;

                            PhotoIDSequence.Clear();

                            for (int i = 1; i <= CONST_repeatPhotoNr; i++)
                            {
                                List<int> secondHalfSequence = GenerateSequence();
                                foreach (int value in secondHalfSequence)
                                {
                                    PhotoIDSequence.Add(value + (i - 1) * CONST_photoNr );
                                }
                            }

                            CurrentIndex = 0;
                            SetCurrentPositionAndImageUrl();

                        }
                        #endregion
                        else
                        #region NormalTrial
                        
                       
                        {
                            CurrentIndex++;

                            if (CurrentIndex < CONST_TrialNr)
                            {
                                SetCurrentPositionAndImageUrl();
                                int crossDuration = GetRandomCrossDuration();
                                literal.Text = string.Format("<script type='text/javascript'> Step1({0},500); </script> ", crossDuration);
                            }
                            else
                            {
                                if (ExtraMySession != null)
                                {
                                    int currentStage = ExtraMySession.Stage;
                                    int newStage = currentStage + 1;// valabil pt trecerea de la 0 la 1 si de la trainingSessions+1 la trainingSessions+2  // acum e va

                                    bool res = DBPool.UpdateUserStage(ExtraMySession.UserID, newStage);
                                    ExtraMySession.Stage = newStage;

                                    int trainingSessions = Int32.Parse(ConfigurationManager.AppSettings["TrainingSessions"].ToString());

                                    //aici se poate lua din bd "campul users.ScreenResolution"

                                    if (newStage == 1)
                                    {
                                        if (Session["DoTraining"] != null && (bool)Session["DoTraining"] == false)
                                        {
                                            int finalStage = trainingSessions + 1;
                                            bool res1 = DBPool.UpdateUserStage(ExtraMySession.UserID, finalStage);
                                            Session.Abandon();
                                            Response.Redirect("~/Exit.aspx", false);
                                            
                                        }
                                        else
                                            Response.Redirect("~/ExitOrContinueToStep2.aspx", false);
                                    }
                                    
                                    //else if (newStage == 3)
                                    else if (newStage == trainingSessions+2)
                                    {
                                        Response.Redirect("~/Thanks.aspx", false);
                                    }
                                }
                                else
                                {
                                    Response.Redirect("~/Login.aspx", false);
                                }
                            }
                        }
                        #endregion

                        #endregion

                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message + "~~~~" + ex.StackTrace;
                if (ex.InnerException != null) error += "@@@@@@" + ex.InnerException.ToString();
                MyLogs.WriteError(error);
                

                throw ex;
            }

        }

        #endregion

        #region Utils

        private int GetRandomCrossDuration()
        {
            Random rand = new Random();
            int n = rand.Next(0, 2);
            switch (n)
            {
                case 0: return 500;
                case 1: return 1250;
                default: return 500;
            }

        }

        private void SetCurrentPositionAndImageUrl()
        {
            //MyLogs.WriteError(" Is test trial:"+ IsTestTrial);
            if (!IsTestTrial)
            {
                int i = (CurrentIndex / CONST_photoNr) + 1;

                CurrentPosition = PhotoIDSequence[CurrentIndex - (i - 1) * CONST_photoNr];


                //CurrentPosition = (PhotoIDSequence[CurrentIndex] < CONST_photoNr) ? PhotoIDSequence[CurrentIndex] : PhotoIDSequence[CurrentIndex - (CONST_repeatPhotoNr - 1) * CONST_photoNr];
            }
            else
            {
                //MyLogs.WriteError(" CurrentTestTrail:" + CurrentTestTrail);
                
                //if (PhotoIDSequence[CurrentTestTrail] < CONST_photoNr)
                //    MyLogs.WriteError(" CurrentPosition:" + PhotoIDSequence[CurrentTestTrail]);
                //else
                    //MyLogs.WriteError(" CurrentPosition:" + PhotoIDSequence[CurrentTestTrail - CONST_photoNr]);
                CurrentPosition = (PhotoIDSequence[CurrentTestTrail] < CONST_photoNr) ? PhotoIDSequence[CurrentTestTrail] : PhotoIDSequence[CurrentTestTrail - CONST_photoNr];
            }

            
            string imageInfo = MapAllPhoto[CurrentPosition];

            img2.ImageUrl = MethodPool.GetImagePath(PhotoFolderType.DotProbe_Photo, imageInfo);

            string photoName= GetPhotoName(imageInfo);
            string[] info = photoName.Split('\\','.');

            bool isCongruent = (info[3] == CONST_congruent);
            bool isRightPhoto = (info[4] == CONST_right);

            img3.ImageUrl = MethodPool.GetImagePath(PhotoFolderType.DotProbe_Target, GetRandomTargetImage(isRightPhoto,isCongruent));
            TargetType targetType = GetTargetType(img3.ImageUrl);

            hIsVerticalDots.Value = (targetType==TargetType.Vertical_Dots)? "1":"0" ;

        }

        private void FillMapAllPhoto()
        {
            MethodPool.FillDirectoryMap(MapAllPhoto,PhotoFolderType.DotProbe_Photo);
            for (int i = 0; i < MapAllPhoto.Count; i++)
            {
                string fullName = MapAllPhoto[i];
                int indexStart = fullName.IndexOf(@"\Photo\") + 7;
                MapAllPhoto[i] = fullName.Substring(indexStart);

            }
        }


        private List<int> GenerateSequence()
        {
            List<int> sequence = new List<int>();
            try
            {
                List<int> l1 = MethodPool.CreateNumericList(0, 7);
                List<int> l2 = MethodPool.CreateNumericList(8, 15);
                List<int> l3 = MethodPool.CreateNumericList(16, 23);

                //List<int> l1 = MethodPool.CreateNumericList(0, 15);
                //List<int> l2 = MethodPool.CreateNumericList(16, 31);
                //List<int> l3 = MethodPool.CreateNumericList(32, 47);

                ////List<int> l4 = MethodPool.CreateNumericList(48, 55);
                ////List<int> l5 = MethodPool.CreateNumericList(56, 63);
                ////List<int> l6 = MethodPool.CreateNumericList(64, 71);

                int x1 = 0;
                int x2 = 0;
                int x3 = 0;
                //int x4 = 0;
                //int x5 = 0;
                //int x6 = 0;

                for (int i = 1; i <= 4; i++)
                {
                    x1 = MethodPool.GetRandomValue(l1);

                    x2 = MethodPool.GetRandomValue(l2);
                    x3 = MethodPool.GetRandomValue(l3);
                    //x4 = MethodPool.GetRandomValue(l4);
                    //x5 = MethodPool.GetRandomValue(l5);
                    //x6 = MethodPool.GetRandomValue(l6);

                    //FillWithRandomSmallList(sequence, new int[] {x1,x2,x3,x4,x5,x6});  
                    FillWithRandomSmallList(sequence, new int[] { x1, x2, x3 });

                    l1.Remove(x1);
                    l2.Remove(x2);
                    l3.Remove(x3);
                    //l4.Remove(x4);
                    //l5.Remove(x5);
                    //l6.Remove(x6);
                }

                for (int i = 1; i <= 4; i++)//de ce?
                {
                    x1 = MethodPool.GetRandomValue(l1);
                    x2 = MethodPool.GetRandomValue(l2);
                    x3 = MethodPool.GetRandomValue(l3);

                    FillWithRandomSmallList(sequence, new int[] { x1, x2, x3 });

                    l1.Remove(x1);
                    l2.Remove(x2);
                    l3.Remove(x3);
                }
                //.OrderBy(x => x).ToList<int>();
                return sequence;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void FillWithRandomSmallList(List<int> bigList, params int[] values)
        {
            List<int> smallList = new List<int>(values as IEnumerable<int>);
            smallList = MethodPool.ShuffleList(smallList);

            if (bigList.Count > 0)
            {
                while (ElementsInSameCategory(smallList[0], bigList[bigList.Count - 1]))
                {
                    smallList = MethodPool.ShuffleList(smallList);
                }
            }

            for (int i = 0; i < smallList.Count; i++)
			{
                bigList.Add(smallList[i]);
			}
            
        }

        private bool ElementsInSameCategory(int x, int y)
        {
            if (x <= 7 && y <= 7) return true;
            if (x > 7 && x <= 15 && y > 7 && y <= 15) return true;
            if (x > 15 && x <= 23 && y > 15 && y <= 23) return true;// poate trebuie y<23

            //if (x <= 15 && y <= 15) return true;
            //if (x > 15 && x <= 31 && y > 15 && y <= 31) return true;
            //if (x > 31 && x <= 47 && y > 31 && y < 47) return true;
            ////if (x > 47 && x <= 55 && y > 47 && y < 55) return true;
            ////if (x > 55 && x <= 63 && y > 55 && y < 63) return true;
            ////if (x > 63 && x <= 71 && y > 63 && y < 71) return true;
            return false;
        }

        
        string GetPhotoName(string photoFullVirtualPath)
        {
            int startIndex = photoFullVirtualPath.LastIndexOf('\\');
            //if (startIndex == -1)
            //{
            //    startIndex = 0;
            //}
            return photoFullVirtualPath.Substring(startIndex + 1);
        }

        string GetRandomTargetImage(bool isRightPhoto, bool isCongruent)
        {
            Random rand = new Random();
            
            int n = rand.Next(0,2); // 0 inseamna horizontal, 1 inseamna vertical


            if (n == 0 && ((isRightPhoto && isCongruent) || (!isRightPhoto && !isCongruent)))
                return "horizontal_points_right.jpg";
            if (n == 1 && ((isRightPhoto && isCongruent) || (!isRightPhoto && !isCongruent)))
                return "vertical_points_right.jpg";
            if (n == 0 && ((!isRightPhoto && isCongruent) || (isRightPhoto && !isCongruent)))
                return "horizontal_points_left.jpg";
            if (n == 1 && ((!isRightPhoto && isCongruent) || (isRightPhoto && !isCongruent)))
                return "vertical_points_left.jpg";

            return "";
        }

        private Emotion GetEmotion(string emotionIndicator)
        {
            switch (emotionIndicator)
            {
                case "HA-AN": return Emotion.Happy_Angry;
                case "AN-HA": return Emotion.Happy_Angry;
                case "N-AN": return Emotion.Neutru_Angry;
                case "N-HA": return Emotion.Neutru_Happy;
                default: return Emotion.Unassigned;
            }
        }

        private Gender GetGender(string genderIndicator)
        {
            switch (genderIndicator)
            {
                case "F": return Gender.Female;
                case "M": return Gender.Male;
                default: return Gender.Unassigned;
            }
        }
        
        private PhotoOrientation GetPhotoOrientation(string photoOrientationIndicator)
        {
            switch (photoOrientationIndicator)
            {
                case "R": return PhotoOrientation.Right;
                case "L": return PhotoOrientation.Left;
                default: return PhotoOrientation.Unassigned;
            }
        }
        
        private bool GetCogruent(string  congruentIndicator)
        {
            switch (congruentIndicator)
            {
                case "CGR": return true;
                case "ICGR": return false;
                default: return false;
            }
        }

        private int GetCode(string codeIndicator)
        {
            int code = 0;
            Int32.TryParse(codeIndicator, out code);
            return code;

        }

        private void GetPhotoIndicators(string photoName, out int code, out Emotion emotion, out Gender gender, out bool isCongruent, out PhotoOrientation photoOrientation)
        {
            string[] indicators = photoName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            code = GetCode(indicators[0]);
            gender = GetGender(indicators[1]);
            emotion = GetEmotion(indicators[2]);
            isCongruent = GetCogruent(indicators[3]);
            photoOrientation = GetPhotoOrientation(indicators[4]);
        }

        private TargetType GetTargetType(string targetPhotoName)
        {
            if (targetPhotoName.ToLower().Contains("horizontal"))
                return TargetType.Horizontal_Dots;
            if (targetPhotoName.ToLower().Contains("vertical"))
                return TargetType.Vertical_Dots;
            return TargetType.Unassigned;
        }

        #endregion

    }
}


