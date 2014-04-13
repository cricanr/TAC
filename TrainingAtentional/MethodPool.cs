using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Globalization;
using System.IO;

namespace TrainingAtentional
{
    public enum TargetType
    {
        Unassigned = 0,
        Horizontal_Dots = 1,
        Vertical_Dots = 2,
    }

    public enum Emotion
    {
        Unassigned = 0,
        Happy = 1,
        Angry = 2,
        Happy_Angry = 3,
        Neutru_Angry = 4,
        Neutru_Happy = 5
    }

    public enum Gender
    {
        Unassigned = 0,
        Female = 1,
        Male = 2
    }

    public enum PhotoOrientation
    {
        Unassigned = 0,
        Left = 1,
        Right = 2
    }

    public enum TargetOrientation
    {
        Left = 1,
        Right = 2
    }

    public enum PhotoFolderType
    {
        DistorsiuneAtentionala_Photo,
        DistorsiuneAtentionala_Target,
        DistorsiuneAtentionala_Empty,
        TrainingAtentional_Photo,
        TrainingAtentional_Target,
        TrainingAtentional_Cross,

        Training_Happy,
        Training_Angry,

        DotProbe,
        DotProbe_Photo,
        DotProbe_Target,

        DotProbe_Out_HappyAngry,
        DotProbe_Out_NeutruAngry,
        DotProbe_Out_NeutruHappy,
        DotProbe_In_HappyAngry,
        DotProbe_In_NeutruAngry,
        DotProbe_In_NeutruHappy,

    }
    public static class MethodPool
    {
        private static List<decimal> percentList = null;

        public static List<decimal> GetPositionPercentagesPercentList(int a, int x, int b, int y, List<int> array)
        {
            if (percentList == null)
            {
                return CalculatePositionPercentages(a, x, b, y, array);
            }
            else
            {
                return percentList;
            }
        }


        public static List<int> GenerateUniqueRandomList(int n)
        {
            ArrayList list = new ArrayList();
            List<int> l = new List<int>(n);

            for (int i = 1; i <= n; i++)
            {
                l.Add(i);
            }
            Random rand = new Random();

            return l.OrderBy(x => rand.Next()).Take(n).ToList<int>();

        }



        public static List<int> GenerateRandomList(int n, int minValue, int maxValue)
        {
            ArrayList list = new ArrayList();
            List<int> l = new List<int>(n);
            Random rand = new Random();

            for (int i = 1; i <= n; i++)
            {
                int value = rand.Next(minValue, maxValue);
                l.Add(value);
            }
            return l;

        }


        public static string GetDirectoryPath(PhotoFolderType photoFolderType)
        {
            string folderPath = "";
            try
            {
                switch (photoFolderType)
                {
                    case PhotoFolderType.DistorsiuneAtentionala_Photo:
                        folderPath = "~/Poze/DistorsiuneAtentionala/Photo";
                        break;
                    case PhotoFolderType.DistorsiuneAtentionala_Target:
                        folderPath = "~/Poze/DistorsiuneAtentionala/Target";
                        break;
                    case PhotoFolderType.DistorsiuneAtentionala_Empty:
                        folderPath = "~/Poze/DistorsiuneAtentionala/Empty";
                        break;
                    case PhotoFolderType.TrainingAtentional_Cross:
                        folderPath = "~/Poze/TrainginAtentional/Cross";
                        break;
                    case PhotoFolderType.TrainingAtentional_Photo:
                        folderPath = "~/Poze/TrainginAtentional/Photo";// e gresit dar nu ajunge aici
                        break;
                    case PhotoFolderType.TrainingAtentional_Target:
                        folderPath = "~/Poze/TrainingAtentional/Target";
                        break;
                    case PhotoFolderType.Training_Angry:
                        folderPath = "~/Poze/Training/Angry";
                        break;
                    case PhotoFolderType.Training_Happy:
                        folderPath = "~/Poze/Training/Happy";
                        break;
                    case PhotoFolderType.DotProbe:
                        folderPath = "~/Poze/DotProbe";
                        break;
                    case PhotoFolderType.DotProbe_Photo:
                        folderPath = "~/Poze/DotProbe/Photo";
                        break;
                    case PhotoFolderType.DotProbe_Target:
                        folderPath = "~/Poze/DotProbe/Target";
                        break;

                    case PhotoFolderType.DotProbe_In_HappyAngry:
                        folderPath = "~/Poze/DotProbe/picture_from_training/happy-angry";
                        break;
                    case PhotoFolderType.DotProbe_In_NeutruAngry:
                        folderPath = "~/Poze/DotProbe/picture_from_training/neutru-angry";
                        break;
                    case PhotoFolderType.DotProbe_In_NeutruHappy:
                        folderPath = "~/Poze/DotProbe/picture_from_training/neutru-happy";
                        break;
                    case PhotoFolderType.DotProbe_Out_HappyAngry:
                        folderPath = "~/Poze/DotProbe/pictures_from_outside_training/happy-angry";
                        break;
                    case PhotoFolderType.DotProbe_Out_NeutruAngry:
                        folderPath = "~/Poze/DotProbe/pictures_from_outside_training/neutru-angry";
                        break;
                    case PhotoFolderType.DotProbe_Out_NeutruHappy:
                        folderPath = "~/Poze/DotProbe/pictures_from_outside_training/neutru-happy";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return folderPath;

        }

        public static string GetImagePath(PhotoFolderType photoFolderType, string imageName)
        {
            string result = "";
            try
            {
                string folderPath = GetDirectoryPath(photoFolderType);
                result = string.Format("{0}/{1}", folderPath, imageName);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static DateTime GetDateFromJS(string JSdate)
        {
            DateTime result = new DateTime();

            try
            {
                string[] fullDate = JSdate.Split(';');
                int year = Int32.Parse(fullDate[0]);
                int month = Int32.Parse(fullDate[1]) + 1;
                int date = Int32.Parse(fullDate[2]);
                int hour = Int32.Parse(fullDate[3]);
                int minute = Int32.Parse(fullDate[4]);
                int second = Int32.Parse(fullDate[5]);
                int milisecond = Int32.Parse(fullDate[6]);

                result = new DateTime(year, month, date, hour, minute, second, milisecond);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }


        // Varianta noua

        public static List<int> ShuffleList(List<int> list)
        {
            List<int> result = new List<int>();
            try
            {
                Random rand = new Random();
                int n = list.Count;
                result = list.OrderBy(x => rand.Next()).Take(n).ToList<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
        /// <summary>
        /// first a values from the array have a sum of x% , the next b values have y%
        /// </summary>
        /// <param name="a"></param>
        /// <param name="x"></param>
        /// <param name="b"></param>
        /// <param name="y"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int GetPositionPercentage(int a, int x, int b, int y, List<int> array, int oldPosition)
        {
            int result = 0;

            try
            {
                int newPosition = oldPosition;

                percentList = CalculatePositionPercentages(a, x, b, y, array);

                Random rand = new Random();
                int generatedNr = rand.Next(0, 101);

                for (int i = 0; i < array.Count; i++)
                {
                    if (percentList[i] >= generatedNr)
                    {
                        newPosition = array[i];
                        break;
                    }
                }

                if (newPosition != oldPosition)
                {
                    return newPosition;
                }
                else
                {
                    result = GetPositionPercentage(a, x, b, y, array, oldPosition);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static void FillFileMap(Dictionary<int, string> map, PhotoFolderType photoFolderType)
        {
            try
            {
                string directoryPath = MethodPool.GetDirectoryPath(photoFolderType);
                int index = 0;

                string physicalPath = HttpContext.Current.Server.MapPath(directoryPath);
                foreach (string filePath in Directory.GetFiles(physicalPath))
                {
                    FileInfo file = new FileInfo(filePath);
                    map.Add(index, file.Name);
                    index++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FillDirectoryMap(Dictionary<int, string> map, PhotoFolderType photoFolderType)
        {
            try
            {
                string directoryPath = MethodPool.GetDirectoryPath(photoFolderType);

                string physicalPath = HttpContext.Current.Server.MapPath(directoryPath);
                FillMap(physicalPath, map);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static void FillMap(string path, Dictionary<int, string> map)
        {
            try
            {

                if (File.Exists(path))
                {
                    ProcessFile("", path, map);
                }

                else if (Directory.Exists(path))
                {
                    ProcessDirectory("", path, map);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static void ProcessDirectory(string extraSubdirectories, string targetDirectory, Dictionary<int, string> map)
        {
            try
            {

                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                {
                    ProcessFile(extraSubdirectories, fileName, map);
                }


                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    DirectoryInfo directory = new DirectoryInfo(subdirectory);
                    extraSubdirectories += directory.Name + "/";
                    ProcessDirectory(extraSubdirectories, subdirectory, map);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void ProcessFile(string additionalSubdirectories, string path, Dictionary<int, string> map)
        {
            try
            {

                FileInfo file = new FileInfo(path);
                //map.Add(map.Count, additionalSubdirectories + file.Name);
                map.Add(map.Count, file.FullName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static List<int> CreateNumericList(int first, int last)
        {


            List<int> list = new List<int>();

            try
            {

                for (int i = first; i <= last; i++)
                {
                    list.Add(i);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetRandomValue(List<int> source)
        {
            int result = 0;
            try
            {

                Random rand = new Random();
                result = source.OrderBy(x => rand.Next()).Take(1).ElementAt<int>(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public static List<decimal> CalculatePositionPercentages(int a, int x, int b, int y, List<int> array)
        {
            percentList = new List<decimal>();
            int length = array.Count;
            if (length != a + b) return null;

            decimal horizontalPercentage = (decimal)x / a;
            for (int i = 0; i < a; i++)
            {
                percentList.Add(horizontalPercentage);
            }

            decimal verticalPercentage = (decimal)y / b;
            for (int i = 0; i < b; i++)
            {
                percentList.Add(verticalPercentage);
            }

            for (int i = 1; i < length; i++)
            {
                percentList[i] += percentList[i - 1];
            }

            //deoarece suma poate da 99,9999
            percentList[length - 1] = 100;

            return percentList;
        }
    }
}