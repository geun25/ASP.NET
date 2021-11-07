-- 해당 아티클을 세부적으로 읽어오는 저장 프로시저
CREATE PROCEDURE [dbo].[AnswersDetails]
	@Id INT 
AS
	--[A] 조회수 카운트 1증가
	UPDATE Answers SET ReadCount = ReadCount + 1 WHERE Id = @Id

	--[B] 모든 항목 조회
	SELECT * FROM Answers WHERE Id = @Id
GO