using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSchool.Models
{
    public enum Score
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        // Entity Framework는 특정 속성의 이름이 <탐색 속성 이름><기본키 속성 이름> 패턴으로
        // 구성되어 있으면 해당 속성을 외래키로 해석한다.
        // 또는 <기본키 속성 이름>을 그대로 외래키 속성 이름으로 사용가능하다.

        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Score? Score { get; set; }

        //탐색 속성
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}