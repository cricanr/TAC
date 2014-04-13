using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingAtentional
{
    [Serializable]
    public class MySession
    {
        int _userID;

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        int _stage;

        public int Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }

        public MySession(int userID,string userName, int stage)
        {
            _userName = userName;
            _userID = userID;
            _stage = stage;
        }
    }
}