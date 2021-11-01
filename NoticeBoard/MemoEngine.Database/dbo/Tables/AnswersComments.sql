--댓글 테이블 생성
CREATE TABLE [dbo].[AnswersComments]
(
	Id			INT	IDENTITY(1,1)
				NOT NULL PRIMARY KEY,	--일련번호
	ArticledId	INT	NOT NULL,			--해당 Article 게시물 번호(BoardId, ProductId)

	--5W1H
	Name			NVARCHAR(25)					NOT NULL,	--이름
	PostDate		DATETIME	DEFAULT GETDATE()	NOT NULL,	--작성일
	PostIp			NVARCHAR(15)					NULL,		--작성IP
	Title			NVARCHAR(150)					NOT NULL,	--제목
	Content			NVARCHAR(MAX)					NOT NULL,	--내용
	Category		NVARCHAR(20)	DEFAULT('Free')	NULL,		--카테고리(확장가능) -> 공지 자유 자료 사진

	--댓글 관련 주요 컬럼: 레거시
	Opinion			NVARCHAR(4000)					NULL,		--댓글 내용
	BoardName		NVARCHAR(50)					NULL,		--게시판 이름(확장) -> Notice, Free, Photo, Data
	Password		NVARCHAR(255)					NOT NULL,	--댓글 삭제용 암호 

	-- 기타 확장 기능 관련 주요 컬럼
	Num				INT								NULL,		--번호(확장...)
	UserId			INT								NULL,		--(확장..) 사용자 테이블 Id
	CategoryId		INT	NULL	DEFAULT 0,						--(확장..) 카테고리 테이블 Id
	BoardId			INT	NULL	DEFAULT 0,						--(확장..) 게시판(Boards) 테이블 Id
	ApplicationId	INT	NULL	DEFAULT 0						--(확장용) 응용 프로그램 Id
)
