-- Connent to winser
 -- select MNIBTrackerDB 
 -- execute to view existing unknown gundata
 -- fill out the information - use null for non-changes
 -- execute to update the gundata


declare @LotNumber varchar(50), @tradedate date, @newTransactionNo varchar(50), @newQuantity int

set @LotNumber = '12345678910112'
set @tradedate = '2015-11-30'
set @newTransactionNo = '1234'
set @newQuantity = null 



update GunData
set TransactionNo = isnull(@newTransactionNo, TransactionNo), Quantity = isnull(@newQuantity,Quantity)
where LotNumber = @LotNumber and cast(TransactionDateTime as date) = @tradedate


select * from UnknownGunData
