using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingAtentional
{
    public class GeneratePosition
    {
        private List<decimal> percentList = null;

        public List<int> ImageOrder { get; set; }
        public int HorizontalPosition { get; set; }
        public int VerticalPosition { get; set; }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public List<decimal> GetPositionPercentagesPercentList()
        {
            if (percentList == null)
            {
                return CalculatePositionPercentages();
            }
            else
            {
                return percentList;
            }
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
        public int GetPositionPercentage(int oldPosition)
        {
            int newPosition = 0;
            try
            {
                newPosition = oldPosition;
                percentList = GetPositionPercentagesPercentList();

                Random rand = new Random();
                int generatedNr = rand.Next(0, 101);

                for (int i = 0; i < ImageOrder.Count; i++)
                {
                    if (percentList[i] >= generatedNr)
                    {
                        newPosition = ImageOrder[i];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newPosition;
        }

        public List<decimal> CalculatePositionPercentages()
        {
            percentList = new List<decimal>();
            int length = ImageOrder.Count;
            if (length != FirstNumber + SecondNumber) return null;

            decimal horizontalPercentage = (decimal)HorizontalPosition / FirstNumber;
            for (int i = 0; i < FirstNumber; i++)
            {
                percentList.Add(horizontalPercentage);
            }

            decimal verticalPercentage = (decimal)VerticalPosition / SecondNumber;
            for (int i = 0; i < SecondNumber; i++)
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