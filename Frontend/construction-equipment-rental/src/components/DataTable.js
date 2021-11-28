import * as React from 'react';
import { DataGrid } from '@mui/x-data-grid';
import TextField from '@mui/material/TextField';

const columns = (selectedItems, onNumberOfDaysChange) => {
    return [
        { field: 'id', headerName: 'ID', flex: 1 },
        { field: 'name', headerName: 'Name', flex: 1 },
        { field: 'equipmentType', headerName: 'Type', flex: 1 },
        {
          field: 'date',
          headerName: 'Number of days',
          flex: 1,
          renderCell: (params) => {
              return <strong>
              <TextField
                id={params.id.toString()}
                label="Number of days"
                type="number"
                disabled={ !selectedItems.includes(params.id) }
                onChange={(event) =>{
                  onNumberOfDaysChange(event.target.id, event.target.value);
                }
               }
              />
            </strong>
          },
        }
      ];
} 

export default function DataTable(props) {
  return (
    <div style={{ height: 370, width: '100%', maxWidth:1400 }}>
      <DataGrid
        rows={props.items}
        columns={columns(props.selectedItems, props.onNumberOfDaysChange)}
        pageSize={5}
        rowsPerPageOptions={[5]}
        checkboxSelection
        disableSelectionOnClick
        onSelectionModelChange={(a)=>{props.onSelectionChange(a)}}
      />
    </div>
  );
}