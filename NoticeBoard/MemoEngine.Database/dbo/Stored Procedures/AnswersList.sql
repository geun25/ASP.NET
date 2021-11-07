--전체 데이터 조회(게시판 리스트)
CREATE PROCEDURE [dbo].[AnswersList]
	@PageNumber INT = 1,
	@PageSize INT = 10
AS
	SELECT *
	FROM Answers
	ORDER BY Ref Desc, RefOrder Asc
	Offset((@PageNumber - 1) * @PageSize) Rows Fetch Next @PageSize Rows Only;
GO