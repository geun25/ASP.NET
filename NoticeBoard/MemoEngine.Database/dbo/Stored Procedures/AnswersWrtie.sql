--게시판 아티클 데이터 저장
CREATE PROCEDURE [dbo].[AnswersWrtie]
	@Name		NVARCHAR(25),
	@PostIp		NVARCHAR(15),
	@Title		NVARCHAR(150),
	@Content	NVARCHAR(MAX),
	@Category	NVARCHAR(10),

	@Email		NVARCHAR(100),
	@Password	NVARCHAR(255),
	@Encoding	NVARCHAR(10),
	@Homepage	NVARCHAR(100),
	@FileName	NVARCHAR(255),
	@FileSize	INT
AS
	
	--[A] Ref열에 일련번호 생성(현재 저장된 Ref중 가장 큰 값에 1을 더해서 증가) 및 그룹화
	DECLARE @MaxRef INT
	SELECT @MaxRef = Max(ISNULL(Ref, 0)) FROM Answers

	If @MaxRef IS NULL
		SET @MaxRef = 1 -- 테이블 생성 후 처음만 비교
	Else
		SET @MaxRef = @MaxRef + 1

	--[B] 만들어진 데이터 저장
	INSERT Answers
	(
		Name, Email, Title, PostIp, Content, Category, 
		Password, Encoding, Homepage, Ref, Step, FileName, FileSize
	)
	VALUES
	(
		@Name, @Email, @Title, @PostIp, @Content, @Category, 
		@Password, @Encoding, @Homepage, @MaxRef, @FileName, @FileSize
	)
GO