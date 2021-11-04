using System.Collections.Generic;

namespace MemoEngine.Answers
{
    public interface IAnswerRepository : IBreadShop<Answer>
    {
        int Delete(int id, string password);

        List<Answer> GetAnswers(int pageNumber, int pageSize = 10 );

        int GetCountAll(); // 전체 레코드 수 반환

        int GetCountBySearch(string searchField, string searchQuery);

        List<Answer> GetSearchAll(string searchField, string searchQuery, int pageNumber, int pageSize = 10);

        List<Answer> GetSummaryByCategory(string category);

        string GetFileNameById(int id);

        List<Answer> GetNewPhotos();

        Answer GetById(int id);

        List<Answer> GetRecentPosts(int number = 5);

        void Pinned(int id);

        void ReplyModel(Answer model);

        int SaveOrUpdate(Answer n, BoardWriteFormType formType);

        void UpdateDownCount(string fileName);

        void UpdateDownCountById(int id);

        int UpdateModel(Answer model);

        Answer WriteModel(Answer model);
    }
}
