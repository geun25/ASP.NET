--게시판 아티클에서 데이터 검색 리스트
CREATE PROCEDURE [dbo].[AnswersSearchList]
	@SearchField NVARCHAR(25),
	@SearchQuery NVARCHAR(25),
	@PageNumber INT = 1,
	@PageSize INT = 10
AS
	SELECT 
		[Id], [Name], [Email], [Title], [PostDate],
		[ReadCount], [Ref], [Step], [RefOrder], [AnswerNum],
		[ParentNum], [CommentCount], [FileName], [FileSize],
		[DownCount]
	FROM Answers
	WHERE (
		CASE @SearchField	
			WHEN 'Name' THEN [Name]
			WHEN 'Title' THEN [Title]
			WHEN 'Content' THEN [Content]
			Else @SearchQuery
		END
	) Like '%' + @SearchQuery + '%'
	Order By Ref Desc, RefOrder Asc
	Offset ((@PageNumber - 1) * @PageSize) Rows Fetch Next @PageSize Rows Only;
GO