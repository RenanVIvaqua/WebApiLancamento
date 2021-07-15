Create Procedure PrcCadastarLancamento             	
		@Valor		float,
		@Descricao  varchar(60),
		@Conta		varchar(30),
		@Tipo		char(1),			
		@Data		datetime		
		
AS

BEGIN	    	      
	   INSERT INTO Lancamento(valor, descricao, conta, tipo, data) 
	   VALUES (@Valor,@Descricao,@Conta,@Tipo,@Data)	      

	   SELECT  SCOPE_IDENTITY()
END
GO



