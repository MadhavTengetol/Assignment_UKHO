select * from Acl 
	left join ReadUsers on acl.Id = ReadUsers.AclId 
	left join Batches on Batches.BatchId = acl.BatchId 

	select Batches.BatchId,Batches.BatchPublicationDate,files.Id,
		   files.FileName,files.FileSize,files.MimeType,Attributes.*,
		   FileAttributes.* 
	from Files
	join Batches on Files.BatchId = Batches.BatchId
	join Attributes on Batches.BatchId = Attributes.BatchId
	join FileAttributes on FileAttributes.FilesId = Files.Id
	where files.Id=1008
	
	select * 