using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using TrainingAtentional.Logs;
using System.Configuration;

namespace TrainingAtentional
{
    public partial class Training : BasePage
    {
        #region Properties

        const int CONST_photoNr = 16;
        const int CONST_repeatPhotoNr = 9;//= 12;//3
        const int CONST_totalTrials = 144;//192//48
        const int CONST_totalTestTrials = 5;//10


        List<int> HPhotoIDSequence
        {
            get { return (List<int>)ViewState["HPhotoIDSequence"]; }
            set { ViewState["HPhotoIDSequence"] = value; }
        }

        int CurrentTrail
        {
            get { return (int)(ViewState["CurrentTrail"] ?? 1); }
            set { ViewState["CurrentTrail"] = value; }
        }

        int CurrentTestTrail
        {
            get { return (int)ViewState["CurrentTestTrail"]; }
            set { ViewState["CurrentTestTrail"] = value; }
        }

        int CurrentHPhotoID
        {
            get { return (IsTestTrial)?(int)HPhotoIDSequence[CurrentTestTrail]: (int)HPhotoIDSequence[CurrentTrail]; }
            
        }

        bool IsTestTrial
        {
            get {

                if (ExtraMySession != null && ExtraMySession.Stage == 1)
                {
                    return CurrentTestTrail <= CONST_totalTestTrials;
                }
                else return false;
            }
        }

        int CurrentBlock{

            get
            {
                if (IsTestTrial)
                    return 0;
                //else if (CurrentTrail >= (decimal)(CONST_totalTrials) / 2 ) 
                //    return 2;
                else
                    return 1;
            }
            
            
        }

        int CurrentHPosition
        {
            get { return (int)ViewState["CurrentHPosition"]; }
            set { ViewState["CurrentHPosition"] = value; }
        }

        int OldHPosition
        {
            get { return (int)ViewState["OldHPosition"]; }
            set { ViewState["OldHPosition"] = value; }
        }

        /// <summary>
        /// Lista de 16 elemente
        /// indexul va reprezenta pozitia din matrice ( i =0 reprezinta poza1 din matrice) , valoarea va reprezenta PhotoID-ul)
        /// </summary>
        List<int> PhotoIDPositionSequence
        {
            get { return (List<int>)ViewState["PhotoPositionSequence"]; }
            set { ViewState["PhotoPositionSequence"] = value; }
        }



        Dictionary<int, string> MapHPhoto
        {
            get { return (Dictionary<int, string>)ViewState["MapHPhoto"]; }
            set { ViewState["MapHPhoto"] = value; }
        }
        Dictionary<int, string> MapAPhoto
        {
            get { return (Dictionary<int, string>)ViewState["MapAPhoto"]; }
            set { ViewState["MapAPhoto"] = value; }
        }

        #endregion

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //int x = 0;
                //int y = 0;
                //int z = x / y;

                if (!IsPostBack)
                {
                    divInfo.Style["display"] = "";

                    MapHPhoto = new Dictionary<int, string>();
                    MapAPhoto = new Dictionary<int, string>();

                    //Body.Attributes.Add("onkeypress", "OnBodyKeyPressed();");
                    if (ExtraMySession != null && ExtraMySession.Stage == 1)
                        literalDocumentKeyPressed.Text = "<script type='text/javascript'> document.onkeypress = OnDocumentStartTrialKeyPressed; </script> ";
                    else
                        literalDocumentKeyPressed.Text = "<script type='text/javascript'> document.onkeypress = OnDocumentStartNonTestTrialKeyPressed; </script> ";

                    //se calc o singura data
                    FillMapHPhoto(MapHPhoto);
                    FillMapAPhoto(MapAPhoto);
                    SetPhotoProperties();

                    //incarcare pt test
                    HPhotoIDSequence = GenerateHSequence(CONST_photoNr, CONST_repeatPhotoNr);


                    CurrentHPosition = GetHPosition(-1);

                    //CurrentTrail = 0;

                    if (ExtraMySession != null && ExtraMySession.Stage > 1)
                    {
                        CurrentTrail = 0;
                    }

                    CurrentTestTrail = 1;

                    hCurrentHPosition.Value = CurrentHPosition.ToString();
                    PhotoIDPositionSequence = GetPhotoIDSequence(CurrentHPosition, CurrentHPhotoID);

                    //MyLogs.WriteError("CurrentPosition:" + CurrentHPosition);
                    //MyLogs.WriteError("PhotoIDPositionSequence:" + PhotoIDPositionSequence.Count);
                    //MyLogs.WriteError("MapHPhoto:" + MapHPhoto.Count);
                    //MyLogs.WriteError("PhotoIDPositionSequence[0]:" + PhotoIDPositionSequence[0]);
                    //MyLogs.WriteError("MapHPhoto[0]:" + MapHPhoto[0]);

                    FillPhotos(PhotoIDPositionSequence, CurrentHPosition);
                    
                    // TODO: check if this logic is still needed
                    //if (ExtraMySession.Stage == 1)
                        hIsTest.Value = "1";
                    //else
                    //    hIsTest.Value = "0";


                }
                else
                {
                    divInfo.Style["display"] = "none";

                    literalDocumentKeyPressed.Text = "<script type='text/javascript'> document.onkeypress = ''; </script> ";
                    //Body.Attributes.Remove("onkeypress");

                    string eventTarget = Request.Params.Get("__EVENTTARGET");

                    #region correctPositionClicked

                    if (eventTarget.Contains("correctPositionClicked"))
                    {
                        #region Insert in DB

                        //string keyPressed = eventTarget.Split('|')[1];

                        int userID = ExtraMySession.UserID;
                        DateTime dtStart = MethodPool.GetDateFromJS(hStart.Value);
                        DateTime dtStop = MethodPool.GetDateFromJS(hStop.Value);

                        string photoName = MapHPhoto[CurrentHPosition];
                        Gender gender = new Gender();
                        Emotion emotion = new Emotion();
                        int code = 0;
                        GetPhotoIndicators(photoName, out code, out emotion, out gender);


                        string wrongPhotoName = null;
                        DateTime dtStopWrongPosition = new DateTime();
                        int wrongPhotoPosition = 0;

                        if (hWrongPosition.Value != "" && hStopWrongPosition.Value != "")
                        {
                            wrongPhotoPosition = Int32.Parse(hWrongPosition.Value);
                            int wrongPhotoID = PhotoIDPositionSequence[wrongPhotoPosition];
                            wrongPhotoName = GetPhotoNameFromPhotoID(wrongPhotoID, Emotion.Angry);
                            dtStopWrongPosition = MethodPool.GetDateFromJS(hStopWrongPosition.Value);
                        }



                        int result = DBPool.InsertTrainingTrialNew(userID, Session.SessionID, dtStart, dtStop, photoName, code, (int)gender, (int)emotion, CurrentHPosition, CurrentBlock, IsTestTrial, wrongPhotoPosition, wrongPhotoName, dtStopWrongPosition);
                        if (result != 1)
                        {
                            throw new ApplicationException("eroare la inserare training trail");
                        }
                        #endregion

                        hWrongPosition.Value = "";
                        hStopWrongPosition.Value = "";

                        #region Prepare next Step

                        #region CazuriTestTrail

                        if (ExtraMySession != null && ExtraMySession.Stage == 1)
                        {
                            if (CurrentTestTrail < CONST_totalTestTrials)//
                            {
                                CurrentTestTrail++;

                                CurrentHPosition = GetHPosition(CurrentHPosition);
                                hCurrentHPosition.Value = CurrentHPosition.ToString();
                                PhotoIDPositionSequence = GetPhotoIDSequence(CurrentHPosition, CurrentHPhotoID);
                                FillPhotos(PhotoIDPositionSequence, CurrentHPosition);
                                literal.Text = "<script type='text/javascript'> Step1(500); </script> ";

                            }
                            else
                                if (CurrentTestTrail == CONST_totalTestTrials)//s-a salvat in BD si ultimul test
                                {
                                    CurrentTestTrail++;//= maxTestTrial + 1

                                    literalDocumentKeyPressed.Text = "<script type='text/javascript'>document.onkeypress = ''; PrepareRealTrials(); </script> ";
                                    hIsTest.Value = "0";
                                    literal.Text = "";

                                    HPhotoIDSequence = GenerateHSequence(CONST_photoNr, CONST_repeatPhotoNr);//se incarca o noua lista
                                    CurrentTrail = 0;

                                    CurrentHPosition = GetHPosition(-1);
                                    hCurrentHPosition.Value = CurrentHPosition.ToString();
                                    PhotoIDPositionSequence = GetPhotoIDSequence(CurrentHPosition, CurrentHPhotoID);
                                    FillPhotos(PhotoIDPositionSequence, CurrentHPosition);
                                    //literal.Text = "<script type='text/javascript'> Step1(500); </script> ";


                                }
                        }
                       

                       

                        #endregion
                            
                        #region NormalTrial


                        if ((ExtraMySession != null && ExtraMySession.Stage == 1 && CurrentTestTrail > CONST_totalTestTrials) || (ExtraMySession != null && ExtraMySession.Stage > 1)) //s-a salvat in BD si ultimul test
                                {
                                    CurrentTrail++;
                                    if (CurrentTrail < CONST_totalTrials)//198
                                    {
                                        CurrentHPosition = GetHPosition(CurrentHPosition);
                                        hCurrentHPosition.Value = CurrentHPosition.ToString();
                                        PhotoIDPositionSequence = GetPhotoIDSequence(CurrentHPosition, CurrentHPhotoID);
                                        FillPhotos(PhotoIDPositionSequence, CurrentHPosition);

                                        //if (CurrentTrail == CONST_totalTrials / 2)
                                        //{
                                        //    literal.Text = "<script type='text/javascript'> TakeBreak(); </script> ";
                                        //}
                                        //else
                                        {
                                            literal.Text = "<script type='text/javascript'> Step1(500); </script> ";
                                        }

                                    }
                                    else
                                    {

                                        if (ExtraMySession != null)
                                        {
                                            int currentStage = ExtraMySession.Stage;
                                            int newStage = currentStage + 1;// valabil pt trecerea de la 1 la 2

                                            bool res = DBPool.UpdateUserStage(ExtraMySession.UserID, newStage);
                                            if (!res)
                                            {
                                                Response.Redirect("~/Login.aspx?y=7", false);
                                            }
                                            else
                                            {
                                                ExtraMySession.Stage = newStage;

                                                //if (newStage == 2)

                                                int trainingSessions = Int32.Parse(ConfigurationManager.AppSettings["TrainingSessions"].ToString());

                                                if (newStage == trainingSessions + 1)
                                                {
                                                    Response.Redirect("~/ExitOrContinueToStep3.aspx", false);
                                                }
                                                else
                                                {
                                                    //aici trebuie stabilit ce trebuie sa apara dupa ce a terminat sesiunea asta.
                                                    Session.Abandon();
                                                    
                                                    Response.Redirect("~/ExitSessionTraining.aspx", false);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            Response.Redirect("~/Login.aspx",false);
                                        }


                                    }
                                }
                                #endregion

                        #endregion
                    }

                    #endregion

                    #region breakEnded

                    if (eventTarget.Contains("breakEnded"))
                    {
                        #region Insert in DB

                        //string keyPressed = eventTarget.Split('|')[1];
                        int userID = ExtraMySession.UserID;
                        DateTime dtStart = MethodPool.GetDateFromJS(hStart.Value);
                        DateTime dtStop = MethodPool.GetDateFromJS(hStop.Value);


                        int result = DBPool.InsertTrainingBreakNew(userID, Session.SessionID, dtStart, dtStop);
                        if (result != 1)
                        {
                            throw new ApplicationException("eroare la inserare training break");
                        }
                        #endregion

                        literal.Text = "<script type='text/javascript'> Step1(500); </script> ";
                    }

                    #endregion
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

        private List<int> GenerateHSequence(int photoNr, int RepeatPhotoNr)
        {
            List<int> sequence = new List<int>();

            for (int l = 0; l < RepeatPhotoNr; l++)
            {
                for (int i = 0; i < photoNr; i++)
                {
                    sequence.Add(i);
                }
            }

            return MethodPool.ShuffleList(sequence);
        }

        private int GetHPosition(int currentHPosition)
        {
            int newPosition = 0;
            try
            {
                
                var generatePosition = new GeneratePosition();
                generatePosition.ImageOrder = new List<int>() { 5, 6, 10, 11, 0, 1, 2, 3, 4, 7, 8, 9, 12, 13, 14, 15 };
                generatePosition.FirstNumber = 4;
                generatePosition.HorizontalPosition = 15;
                generatePosition.SecondNumber = 12;
                generatePosition.VerticalPosition = 85;
                newPosition = generatePosition.GetPositionPercentage(currentHPosition);

                while (newPosition == currentHPosition)
                {
                    newPosition = generatePosition.GetPositionPercentage(currentHPosition);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newPosition;
        }

        private List<int> GetPhotoIDSequence(int currentHPosition, int currentHPhotoID)
        {
            //generam o lista shuffle pentru celelalte 15 poze

            List<int> sequence = new List<int>();

            try
            {

                int angryPhotoNr = CONST_photoNr - 1;

                for (int i = 0; i < angryPhotoNr; i++)
                {
                    if (i < currentHPhotoID)
                        sequence.Add(i);
                    else
                        sequence.Add(i + 1);
                }


                sequence = MethodPool.ShuffleList(sequence);
                sequence.Insert(currentHPosition, currentHPhotoID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sequence;

        }

        private void FillPhotos(List<int> photoIDSequence, int currentHPosition)
        {
            try
            {

                for (int i = 0; i < photoIDSequence.Count; i++)
                {
                    Image img = (Image)divImages.FindControl(string.Format("img{0}", i));
                    int photoID = photoIDSequence[i];

                    if (i == currentHPosition)
                    {
                        img.ImageUrl = MethodPool.GetImagePath(PhotoFolderType.Training_Happy, MapHPhoto[photoID]);
                    }
                    else
                    {
                        img.ImageUrl = MethodPool.GetImagePath(PhotoFolderType.Training_Angry, MapAPhoto[photoID]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillMapHPhoto(Dictionary<int, string> mapPhoto)
        {
            MethodPool.FillFileMap(mapPhoto, PhotoFolderType.Training_Happy);
        }
        private void FillMapAPhoto(Dictionary<int, string> mapPhoto)
        {
            MethodPool.FillFileMap(mapPhoto, PhotoFolderType.Training_Angry);
        }

      

        private void SetPhotoProperties()
        {

            imgCross.Height = Unit.Pixel(535);
            imgCross.Width = Unit.Pixel(535);

            for (int i = 0; i < 16; i++)
			{
                Image img = (Image)divImages.FindControl(string.Format("img{0}", i));
                img.Width = Unit.Pixel(90);//140//100//67//90
                img.Height = Unit.Pixel(129);//200//143//96//129
                
                
			}
            
        }

        private void SetDivPhotoProperties()
        {
            for (int i = 0; i < 16; i++)
			{
                HtmlGenericControl div = (HtmlGenericControl)divImages.FindControl(string.Format("div{0}", i));
                //
			}
            
        }

        private void GetPhotoIndicators( string photoName, out int code, out Emotion emotion, out Gender gender)
        {
            try
            {

                string[] indicators = photoName.Split(new string[] { "_", "." }, StringSplitOptions.RemoveEmptyEntries);
                code = GetCode(indicators[0]);
                gender = GetGender(indicators[1]);
                emotion = GetEmotion(indicators[2]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


        private Emotion GetEmotion(string emotionIndicator)
        {
            switch (emotionIndicator)
            {
                case "AN": return Emotion.Angry;
                case "HA": return Emotion.Happy;
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

        private int GetCode(string codeIndicator)
        {
            int code = 0;

            Int32.TryParse(codeIndicator, out code);
            return code;

        }

        private string GetPhotoNameFromPhotoID(int photoID, Emotion emotion)
        {
            try
            {
                if (emotion == Emotion.Angry)
                    return MapAPhoto[photoID];
                if (emotion == Emotion.Happy)
                    return MapHPhoto[photoID];
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

     
    }
}