using Application.Models;
using System.Collections.Generic;

namespace Application.Data
{
    public class DocumentData
    {
        public List<Document> Documents
        {
            get
            {
                return new List<Document>
                {
                    new Document{Document_Number = 1, Title = "공지사항", Writer="홍길동"},
                    new Document{Document_Number = 2, Title = "제목 #1", Writer="이순신"},
                    new Document{Document_Number = 3, Title = "제목 #2", Writer="신사임당"},
                    new Document{Document_Number = 4, Title = "제목 #3", Writer="이율곡"},
                    new Document{Document_Number = 5, Title = "제목 #4", Writer="정약용"},
                };
            }
        }
    }
}