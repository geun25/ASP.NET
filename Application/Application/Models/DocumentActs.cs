using Application.Data;
using System.Collections.Generic;

namespace Application.Models
{
    public class DocumentActs
    {
        public List<Document> GetDocuments()
        {
            DocumentData documentData = new DocumentData();
            var documents = documentData.Documents;
            
            return documents;
        }
    }
}