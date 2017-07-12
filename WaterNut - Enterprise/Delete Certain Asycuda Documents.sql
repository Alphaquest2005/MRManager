delete from xcuda_Item
where ASYCUDA_Id in (
SELECT xcuda_ASYCUDA_ExtendedProperties.ASYCUDA_Id
FROM     AsycudaDocumentSet INNER JOIN
                  xcuda_ASYCUDA_ExtendedProperties ON 
                  AsycudaDocumentSet.AsycudaDocumentSetId = xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSetId
WHERE  (AsycudaDocumentSet.AsycudaDocumentSetId = 1149))

delete from xcuda_ASYCUDA
where ASYCUDA_Id in (
SELECT xcuda_ASYCUDA_ExtendedProperties.ASYCUDA_Id
FROM     AsycudaDocumentSet INNER JOIN
                  xcuda_ASYCUDA_ExtendedProperties ON 
                  AsycudaDocumentSet.AsycudaDocumentSetId = xcuda_ASYCUDA_ExtendedProperties.AsycudaDocumentSetId
WHERE  (AsycudaDocumentSet.AsycudaDocumentSetId = 1149))