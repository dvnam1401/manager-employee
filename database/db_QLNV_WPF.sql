USE [db_QLNV_WPF]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 6/21/2023 3:19:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[id] [varchar](10) NOT NULL,
	[firstname] [nvarchar](50) NULL,
	[lastname] [nvarchar](50) NULL,
	[gender] [bit] NULL,
	[address] [nvarchar](150) NULL,
	[birthday] [date] NULL,
	[avatar] [nvarchar](250) NULL,
 CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[employee] ([id], [firstname], [lastname], [gender], [address], [birthday], [avatar]) VALUES (N'E0001', N'Nguyễn Hoàng', N'Hải', 1, N'Biên Hòa - Đồng Nai - Việt Nam', CAST(N'1988-03-05' AS Date), N'C:\Users\nguye\Downloads\anh-2.jpg')
INSERT [dbo].[employee] ([id], [firstname], [lastname], [gender], [address], [birthday], [avatar]) VALUES (N'E0002', N'Trịnh Quốc', N'Thu', 0, N'Biên Hòa', CAST(N'2022-10-09' AS Date), N'C:\Users\nguye\Downloads\crudWPF\crudWPF\crudWPF\bin\Debug\\img\no_avatar.jpg')
INSERT [dbo].[employee] ([id], [firstname], [lastname], [gender], [address], [birthday], [avatar]) VALUES (N'E0005', N'Nguyễn Thị Cẩm', N'Tú', 0, N'Kiên Giang', CAST(N'1988-03-05' AS Date), N'D:\Documents\Desktop\hinhthe\images (1).jpg')
GO
