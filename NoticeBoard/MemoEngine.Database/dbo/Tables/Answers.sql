-- 게시판용 테이블 설계
CREATE TABLE [dbo].[Answers]
(
	[Id]	INT	NOT NULL	PRIMARY KEY	IDENTITY(1,1),	--일련번호

	--5W1H
	Name			NVARCHAR(25)					NOT NULL,	--이름
	PostDate		DATETIME	DEFAULT GETDATE()	NOT NULL,	--작성일
	PostIp			NVARCHAR(15)					NULL,		--작성IP
	Title			NVARCHAR(150)					NOT NULL,	--제목
	Content			NVARCHAR(MAX)					NOT NULL,	--내용
	Category		NVARCHAR(20)	DEFAULT('Free')	NULL,		--카테고리(확장가능)

	-- 기본형 게시판 관련 주요 컬럼
	Email			NVARCHAR(100)					NULL,		--이메일
	Password		NVARCHAR(255)					NULL,		--비밀번호
	ReadCount		INT			DEFAULT 0,						--조회수
	Encoding		NVARCHAR(20)					NOT NULL,	--인코딩(HTML/Text/Mixed)
	Homepage		NVARCHAR(100)					NULL,		--홈페이지
	ModifyDate		DATETIME						NULL,		--수정일
	ModifyIP		NVARCHAR(15)					NULL,		--수정IP
	CommentCount	INT			DEFAULT 0,						--댓글수
	
	-- 자료실 게시판 관련 주요 컬럼
	FileName		NVARCHAR(255)					NULL,		--파일명
	FileSize		INT			DEFAULT 0,						--파일크기
	DownCount		INT			DEFAULT 0,						--다운수

	--답변형 게시판 관련 주요 컬럼
	Ref				INT								NOT NULL,	--참조(부모글)
	Step			INT			DEFAULT 0,						--답변깊이(레벨)
	RefOrder		INT			DEFAULT 0,						--답변순서
	AnswerNum		INT			DEFAULT 0,						--답변수
	ParentNum		INT			DEFAULT 0,						--부모글번호

	-- 기타 확장 기능 관련 주요 컬럼
	Num				INT								NULL,		--번호(확장...)
	UID				INT								NULL,		--Users UID
	UserId			INT								NULL,		--(확장..) 사용자 테이블 Id
	UserName		NVARCHAR(25)					NULL,		--사용자 아이디
	DivisionId		INT			DEFAULT 0			NULL,		--서브 카테고리
	CategoryId		INT	NULL	DEFAULT 0,						--(확장..) 카테고리 테이블 Id
	BoardId			INT	NULL	DEFAULT 0,						--(확장..) 게시판(Boards) 테이블 Id
	ApplicationId	INT	NULL	DEFAULT 0						--(확장용) 응용 프로그램 Id
)
