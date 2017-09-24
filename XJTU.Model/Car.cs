using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XJTU.Model;

namespace XJTU.Model
{
    public class Car: BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal lng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal lat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int satellites { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal accx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal accy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal accz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal magx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal magy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal magz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal memsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal memsy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal memsz { get; set; }

    }
}
