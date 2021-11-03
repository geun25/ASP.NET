/*
MemoEngine의 배포 스크립트

이 코드는 도구를 사용하여 생성되었습니다.
파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
변경 내용이 손실됩니다.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "MemoEngine"
:setvar DefaultFilePrefix "MemoEngine"
:setvar DefaultDataPath "C:\Users\rlaeh\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\rlaeh\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
SQLCMD 모드가 지원되지 않으면 SQLCMD 모드를 검색하고 스크립트를 실행하지 않습니다.
SQLCMD 모드를 설정한 후에 이 스크립트를 다시 사용하려면 다음을 실행합니다.
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'이 스크립트를 실행하려면 SQLCMD 모드를 사용하도록 설정해야 합니다.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'테이블 [dbo].[Answers]을(를) 변경하는 중...';


GO
ALTER TABLE [dbo].[Answers] ALTER COLUMN [Content] NVARCHAR (MAX) NOT NULL;


GO
PRINT N'테이블 [dbo].[AnswersComments]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[AnswersComments] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [ArticledId]    INT             NOT NULL,
    [Name]          NVARCHAR (25)   NOT NULL,
    [PostDate]      DATETIME        NOT NULL,
    [PostIp]        NVARCHAR (15)   NULL,
    [Title]         NVARCHAR (150)  NOT NULL,
    [Content]       NVARCHAR (MAX)  NOT NULL,
    [Category]      NVARCHAR (20)   NULL,
    [Opinion]       NVARCHAR (4000) NULL,
    [BoardName]     NVARCHAR (50)   NULL,
    [Password]      NVARCHAR (255)  NOT NULL,
    [Num]           INT             NULL,
    [UserId]        INT             NULL,
    [CategoryId]    INT             NULL,
    [BoardId]       INT             NULL,
    [ApplicationId] INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'DEFAULT 제약 조건 [dbo].[AnswersComments]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[AnswersComments]
    ADD DEFAULT GETDATE() FOR [PostDate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[AnswersComments]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[AnswersComments]
    ADD DEFAULT ('Free') FOR [Category];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[AnswersComments]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[AnswersComments]
    ADD DEFAULT 0 FOR [CategoryId];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[AnswersComments]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[AnswersComments]
    ADD DEFAULT 0 FOR [BoardId];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[AnswersComments]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[AnswersComments]
    ADD DEFAULT 0 FOR [ApplicationId];


GO
PRINT N'업데이트가 완료되었습니다.';


GO
