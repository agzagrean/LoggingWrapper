CREATE TABLE [dbo].[IntegrationLogging](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LogLevel] [nvarchar](10) NULL,
	[MessageDirection] [nvarchar](10) NULL,
	[ThirdPartySystemId] [nvarchar](20) NULL,
	[ThirdPartySystemType] [nvarchar](20) NULL,
	[PropertyCode] [nvarchar](50) NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
	[Component] [nvarchar](255) NULL,
	[Request] [nvarchar](max) NULL,
	[Response] [nvarchar](max) NULL,
	[Message] [nvarchar](255) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.IntegrationLogging] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)) 

GO