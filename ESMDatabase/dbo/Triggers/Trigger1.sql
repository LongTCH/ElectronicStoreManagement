CREATE TRIGGER [TrgInsertLaptop]
	ON [dbo].[LAPTOP]
	FOR INSERT
	AS
	BEGIN
		declare @x char(9)
		select @x = Id from inserted
		insert USED_PRODUCT_ID values(@x)
		SET NOCOUNT ON
	END
go
CREATE TRIGGER [TrgInsertMonitor]
	ON [dbo].[MONITOR]
	FOR INSERT
	AS
	BEGIN
		declare @x char(9)
		select @x = Id from inserted
		insert USED_PRODUCT_ID values(@x)
		SET NOCOUNT ON
	END
go
CREATE TRIGGER [TrgInsertPC]
	ON [dbo].[PC]
	FOR INSERT
	AS
	BEGIN
		declare @x char(9)
		select @x = Id from inserted
		insert USED_PRODUCT_ID values(@x)
		SET NOCOUNT ON
	END
	go
CREATE TRIGGER [TrgInsertPCCPU]
	ON [dbo].[PCCPU]
	FOR INSERT
	AS
	BEGIN
		declare @x char(9)
		select @x = Id from inserted
		insert USED_PRODUCT_ID values(@x)
		SET NOCOUNT ON
	END
	go
CREATE TRIGGER [TrgInsertPCHarddisk]
	ON [dbo].[PCHARDDISK]
	FOR INSERT
	AS
	BEGIN
		declare @x char(9)
		select @x = Id from inserted
		insert USED_PRODUCT_ID values(@x)
		SET NOCOUNT ON
	END
	go
CREATE TRIGGER [TrgInsertSmartphone]
	ON [dbo].[SMARTPHONE]
	FOR INSERT
	AS
	BEGIN
		declare @x char(9)
		select @x = Id from inserted
		insert USED_PRODUCT_ID values(@x)
		SET NOCOUNT ON
	END
	go
CREATE TRIGGER [TrgInsertVGA]
	ON [dbo].[VGA]
	FOR INSERT
	AS
	BEGIN
		declare @x char(9)
		select @x = Id from inserted
		insert USED_PRODUCT_ID values(@x)
		SET NOCOUNT ON
	END