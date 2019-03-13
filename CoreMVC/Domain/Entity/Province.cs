using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Domain.Entity
{
    public class Province
    {
        /// <summary>
        /// 省份id
        /// </summary>
        public long ProvinceId { get; set; }

        /// <summary>
        /// 省份名
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 学校
        /// </summary>
        public IList<School> School { get; set; }
    }
}
