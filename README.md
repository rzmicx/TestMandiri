ini git yang dibuat untuk mengerjakan test jadi hanya akan 1 branch dan tidak mungkin ada pembaruan setelah dari test ini

untuk sql bisa memakai query dibawah ini :

CREATE TABLE msuser(id INT IDENTITY PRIMARY KEY , username varchar(50), passcode VARBINARY(max),active bit)

CREATE TABLE msdetail_user(id INT IDENTITY PRIMARY KEY, idUser INT ,FOREIGN KEY (idUser) REFERENCES dbo.msuser(id),nama VARCHAR(MAX),umur INT , tanggallahir DATETIME)

CREATE TABLE msitem(id CHAR(5) PRIMARY KEY , namaitem VARCHAR(100) , qty INT NOT NULL )

CREATE TABLE TrTransaction (id INT IDENTITY PRIMARY KEY, idUser INT, idItem CHAR(5) ,FOREIGN KEY (idUser) REFERENCES dbo.msuser(id),FOREIGN KEY (iditem) REFERENCES dbo.msitem(id),qty INT , createby VARCHAR(MAX), createat DATETIME)

ALTER TABLE dbo.msuser 
ADD active bit

CREATE NONCLUSTERED INDEX noncluster_msuser_username ON dbo.msuser(username);

CREATE NONCLUSTERED INDEX noncluster_msdetail_user_idUser ON dbo.msdetail_user(idUser); 

CREATE NONCLUSTERED INDEX noncluster_TrTransaction_idUser ON dbo.TrTransaction(idUser);

CREATE NONCLUSTERED INDEX noncluster_TrTransaction_idItem ON dbo.TrTransaction(idItem);


INSERT INTO msitem (id, namaitem, qty)
VALUES
  ('APL11', 'Apel', FLOOR(RAND() * 100) + 1), 
  ('JAM11', 'Jambu', FLOOR(RAND() * 100) + 1), 
  ('DUR11', 'Jeruk', FLOOR(RAND() * 100) + 1);


alter VIEW vw_griduser as 
SELECT  dates, username ,nama , STRING_AGG( item ,',') AS item,SUM(total) total FROM( 
SELECT CAST(c.createat AS date) dates, a.username ,b.nama ,  d.namaitem  AS item,SUM(c.qty) total  FROM dbo.msuser a JOIN dbo.msdetail_user b ON a.id = b.idUser JOIN dbo.TrTransaction c ON c.idUser=a.id JOIN dbo.msitem d ON c.idItem=d.id
GROUP BY CAST(c.createat AS date), a.username,b.nama,d.namaitem ) AS qty
GROUP BY dates, username,nama 

alter PROC sp_GridData @take int , @orderby varchar(50)='' AS
begin
DECLARE @ordering VARCHAR(50) = ' order by dates asc '
IF(@orderby <> '')
SET @ordering = ' order by '+ @orderby

EXEC('select top '+@take+' convert(varchar,dates,103)as date, username,nama,item,total from vw_griduser '+@ordering)
END

EXEC sp_GridData 5 ,'username'
 SELECT * FROM dbo.msuser
 SELECT * FROM dbo.msdetail_user
 SELECT * FROM dbo.msitem
 SELECT * FROM dbo.TrTransaction




