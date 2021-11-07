-- 게시판 아티클의 검색 결과의 레코드 수 반환
CREATE PROCEDURE [dbo].[AnswersSearchCount]
	@SearchField NVARCHAR(25),
	@SearchQuery NVARCHAR(25)
AS
	SET @SearchQuery = '%' + @SearchQuery + '%'

	SELECT COUNT(*)
	FROM Answers
	WHERE
	(
		CASE @SearchField	
			WHEN 'Name' THEN [Name]
			WHEN 'Title' THEN [Title]
			WHEN 'Content' THEN [Content]
			Else @SearchQuery
		END
	)
	Like
	@SearchQuery
GO
