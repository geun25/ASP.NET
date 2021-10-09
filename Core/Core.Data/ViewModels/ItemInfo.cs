﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.ViewModels
{
    /// <summary>
    /// 품목정보 클래스
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// 상품번호
        /// </summary>
        public Guid ItemNo { get; set; }

        /// <summary>
        /// 상품명
        /// </summary>
        public string ItemName { get; set; }
    }
}
