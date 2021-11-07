--게시판 아티클에 답변 저장
CREATE PROCEDURE [dbo].[AnswersReply]
	@Name		NVARCHAR(25),
	@PostIp		NVARCHAR(15),
	@Title		NVARCHAR(150),
	@Content	NVARCHAR(MAX),
	@Category	NVARCHAR(10) = '',

	@Email		NVARCHAR(100),
	@Password	NVARCHAR(255),
	@Encoding	NVARCHAR(10),
	@Homepage	NVARCHAR(100),
	@ParentNum	INT,	--부모글의 고유번호(Id)
	@FileName	NVARCHAR(255),
	@FileSize	INT
AS
	--[0] 변수 선언
	DECLARE @MaxRefOrder INT
	DECLARE @MaxRefAnswerNum INT
	DECLARE @ParentRef INT
	DECLARE @ParentStep INT
	DECLARE @ParentRefOrder INT

	--[1] 부모글의 답변수(AnswerNum)를 1증가
	UPDATE Answers SET AnswerNum = AnswerNum + 1 WHERE Id = @ParentNum
	
	--[2] 같은 글에 대해서 답변을 두번 이상 하면 먼저 답변한것이 위에 나타남.
	SELECT @MaxRefOrder = RefOrder, @MaxRefAnswerNum = AnswerNum FROM Answers
	WHERE
		ParentNum = @ParentNum AND
		RefOrder = (SELECT MAX(RefOrder) FROM Answers WHERE ParentNum = @ParentNum)

	If @MaxRefOrder Is Null
	Begin
		SELECT @MaxRefOrder = RefOrder FROM Answers WHERE Id = @ParentNum
		Set @MaxRefAnswerNum = 0
	END

	--[3] 중간에 답변달 때(비집고 들어갈 자리 마련)
	SELECT @ParentRef = Ref, @ParentStep = Step FROM Answers WHERE Id = @ParentNum
	UPDATE Answers SET RefOrder = RefOrder + 1
	WHERE Ref = @ParentRef AND RefOrder > (@MaxRefOrder + @MaxRefAnswerNum)

	--[4] 최종저장
	INSERT Answers
	(
		Name, Email, Title, PostIp, Content, Password, Encoding,
		Homepage, Ref, Step, RefOrder, ParentNum, FileName, FileSize,
		Category
	)
	VALUES
	(
		@Name, @Email, @Title, @PostIp, @Content, @Password, @Encoding,
		@Homepage, @ParentRef, @ParentStep + 1, 
		@MaxRefOrder + @MaxRefAnswerNum + 1, @ParentNum, @FileName, @FileSize,
		@Category
	)
GO