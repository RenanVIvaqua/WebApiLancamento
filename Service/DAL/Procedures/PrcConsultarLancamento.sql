Create Procedure PrcConsultarLancamento
    @Data               datetime
AS

BEGIN	    	      
	   SELECT 
	   id,
	   valor,
	   descricao,
	   conta,
	   tipo,
	   data
	   FROM Lancamento 
	   WHERE Convert(dateTime, data) = Convert(dateTime, @Data)
END

GO



