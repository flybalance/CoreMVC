﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Domain.Entity
{
    public class School
    {
        /// <summary>
        /// 学校代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 校名
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 成立年份
        /// </summary>
        public string Years { get; set; }

        /// <summary>
        /// 学生数量
        /// </summary>
        public int StudentNum { get; set; }

        /// <summary>
        /// 重写ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[Code={Code}, SchoolName={SchoolName}, Years={Years}, StudentNum={StudentNum}]";
        }
    }
}
