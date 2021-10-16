using Application.Models;
using System.Collections.Generic;

namespace Application.Data
{
    public class MemberData
    {
        public List<Member> Members
        {
            get
            {
                return new List<Member>
                {
                    new Member{Member_Number = 1, Id = "Lee", Name="이순신", Password="1234"},
                    new Member{Member_Number = 2, Id = "Jung", Name="정약용", Password="1234"},
                    new Member{Member_Number = 3, Id = "Shin", Name="신사임당", Password="1234"},
                };
            }
        }
    }
}