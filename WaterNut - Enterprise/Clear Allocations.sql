
delete from AsycudaSalesAllocations

update xcuda_Item
set DFQtyAllocated = 0, DPQtyAllocated = 0


update EntryDataDetails
set QtyAllocated = 0

update xcuda_PreviousItem
set QtyAllocated = 0

update SubItems 
set QtyAllocated = 0

select * from AsycudaSalesAllocations with(nolock)