ini git yang dibuat untuk mengerjakan test jadi hanya akan 1 branch dan tidak mungkin ada pembaruan setelah dari test ini

untuk sql bisa memakai query dibawah ini :

CREATE TABLE msuser(id INT IDENTITY PRIMARY KEY , username varchar(50), passcode VARBINARY(max))

CREATE TABLE msdetail_user(id INT IDENTITY PRIMARY KEY, idUser INT ,FOREIGN KEY (idUser) REFERENCES dbo.msuser(id),nama VARCHAR(MAX),umur INT , tanggallahir DATETIME)

CREATE TABLE msitem(id CHAR(5) PRIMARY KEY , namaitem VARCHAR(100) , qty INT NOT NULL )

CREATE TABLE TrTransaction (id INT IDENTITY PRIMARY KEY, idUser INT, idItem CHAR(5) ,FOREIGN KEY (idUser) REFERENCES dbo.msuser(id),FOREIGN KEY (iditem) REFERENCES dbo.msitem(id),qty INT , createby VARCHAR(MAX), createat DATETIME)



CREATE NONCLUSTERED INDEX noncluster_msuser_username ON dbo.msuser(username);

CREATE NONCLUSTERED INDEX noncluster_msdetail_user_idUser ON dbo.msdetail_user(idUser); 

CREATE NONCLUSTERED INDEX noncluster_TrTransaction_idUser ON dbo.TrTransaction(idUser);

CREATE NONCLUSTERED INDEX noncluster_TrTransaction_idItem ON dbo.TrTransaction(idItem);


INSERT INTO msitem (id, namaitem, qty)
VALUES
  ('APL11', 'Apel', FLOOR(RAND() * 100) + 1), 
  ('JAM11', 'Jambu', FLOOR(RAND() * 100) + 1), 
  ('DUR11', 'Jeruk', FLOOR(RAND() * 100) + 1);


CREATE VIEW grid_user as 
SELECT a.id, a.username ,b.nama , STRING_AGG(d.namaitem,',') AS item,SUM(c.qty) total FROM dbo.msuser a JOIN dbo.msdetail_user b ON a.id = b.id JOIN dbo.TrTransaction c ON c.idUser=a.id JOIN dbo.msitem d ON c.idItem=d.id
GROUP BY a.id, a.username,b.nama

create PROC GridData @takes int , @orderby varchar(50) AS
begin
DECLARE @ordering VARCHAR(50) = ' order by a.id asc '
IF(@orderby <> '')
SET @ordering = ' order by '+ @orderby

EXEC('select top '+@takes+' username,nama,item,total from grid_user '+@ordering)
END

EXEC GridData 5 ,'username'
 


