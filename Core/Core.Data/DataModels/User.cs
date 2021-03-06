using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.DataModels
{
    // C# Class => Database Table
    // 멤벼변수 => Table Column

    // 1.Data Annotations
    public class User
    {
        //Primary Key 지정, 컬럼길이 지정, 컬럼유형 지정
        [Key, StringLength(50), Column(TypeName = "varchar(50)", Order = 0)]
        public string UserId { get; set; }

        //Not null 지정, 컬럼길이 지정, 컬럼유형 지정
        [Required, StringLength(100), Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; }

        [Required, StringLength(320), Column(TypeName = "varchar(320)")]
        public string UserEmail { get; set; }

        [Required, StringLength(130), Column(TypeName = "nvarchar(130)")]
        public string Password { get; set; }

        /// <summary>
        /// 회원 탈퇴여부
        /// </summary>
        [Required]
        public bool IsMembershipWithdrawn { get; set; }

        /// <summary>
        /// 가입일자
        /// </summary>
        [Required]
        public DateTime JoinedUtcDate { get; set; }

        //FK 지정
        //사용자별 소유권한 테이블에 대해서 일대다관계
        [ForeignKey("UserId")]
        public virtual ICollection<UserRolesByUser> UserRolesByUsers { get; set; }

    }
}
