using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using OSIsoft.AF.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLECalibrationToolV2.PI_AFSDK
{
    /// <summary>
    ///  Class to update values to PI Server
    /// </summary>
    static class PIUpdate
    {
        /// <summary>
        /// Zipping to List
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="second"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> ZipList<T1, T2, TResult>(this IEnumerable<T1> source, IEnumerable<T2> second, Func<T1, T2, TResult> func)
        {
            using (var e1 = source.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                    yield return func(e1.Current, e2.Current);
            }
        }

        /// <summary>
        /// Method to update PI tags in PI Server based on sensor tags and konicavalues 
        /// </summary>
        /// <param name="sensorTags"></param>
        /// <param name="konikavalues"></param>
        /// <param name="_piServer"></param>
        public static void UpdatePI(string[] sensorTags, string[] konikavalues, PIServer _piServer, string time)
        {
            var sensor = ZipList(sensorTags, konikavalues, (tag, values) => new { tag = tag, values = values }).ToList();
            foreach (var p in sensor)
            {
                var point = PIPoint.FindPIPoint(_piServer, p.tag);
                if (point != null)
                {
                    //point.UpdateValue(new AFValue(p.values, AFTime.Parse(time, null)), AFUpdateOption.Insert);
                   point.UpdateValue(new AFValue(p.values, AFTime.Parse(time, null)), AFUpdateOption.Insert,AFBufferOption.BufferIfPossible);
                }
            }
        }

    }
}
