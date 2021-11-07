--해당 아티클을 수정하는 저장 프로시저 : 수정이 완료되면 1을, 그렇지 않으면 0을 반환
CREATE PROCEDURE [dbo].[AnswersModify]
	@Name		NVARCHAR(25),
	@ModifyIp	NVARCHAR(15),
	@Title		NVARCHAR(150),
	@Content	NVARCHAR(MAX),
	@Category	NVARCHAR(50),
	@Email		NVARCHAR(100),
	@Password	NVARCHAR(255),
	@Encoding	NVARCHAR(10),
	@Homepage	NVARCHAR(100),
	@FileName	NVARCHAR(255),
	@FileSize	INT,
	@Id			INT
AS
	--번호와 암호가 맞으면 수정을 진행
	DECLARE @cnt INT
	SELECT @cnt = COUNT(*) FROM Answers WHERE Id = @Id AND Password = @Password
	
	If @cnt > 0 -- 번호와 암호가 맞는게 있다면
	BEGIN
		UPDATE Answers
		SET
			Name = @Name,
			Email = @Email,
			Title = @Title,
			ModifyIp = @ModifyIp,
			ModifyDate = GetDate(),
			Content = @Content,
			Encoding = @Encoding,
			Homepage = @Homepage,
			FileName = @FileName,
			FileSize = @FileSize
		WHERE Id = @Id

		SELECT '1'
	END
	Else
	BEGIN
		SELECT '0'
	END
GO