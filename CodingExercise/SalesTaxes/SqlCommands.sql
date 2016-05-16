IF ( OBJECT_ID('dbo.sp_Basket_DEL_byPK') IS NOT NULL ) 
   DROP PROCEDURE dbo.sp_Basket_DEL_byPK
GO

CREATE PROCEDURE dbo.sp_Basket_DEL_byPK
       @basket_id    INT        
AS 
BEGIN 
     SET NOCOUNT ON 

     DELETE
     FROM   dbo.Baskets
     WHERE  
     basket_id = @basket_id

END
GO