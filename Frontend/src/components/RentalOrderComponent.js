import * as React from 'react';
import DataTable from './DataTable';
import Button from '@mui/material/Button';
import FormDialog from './Dialog'
import { GeneratePdf } from './helper';
import { Alert } from '@mui/material';
import config from '../config.json'

export default class RentalOrderComponent extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        error: null,
        isLoaded: false,
        items: [],
        selectedItems: [],
        selectedDays: [],
        lastOrderId: null,
        openDialog: false,
        displayAlert: false,
        alertText: null
      };
    }

    getUrl(endpoint){
      return `${config.serverUrl}/v${config.apiVersion}/${endpoint}`
    }
  
    componentDidMount() {
      fetch(this.getUrl(config.equipmentEndpoint))
        .then(res => res.json())
        .then(
          (result) => {
            this.setState({
              isLoaded: true,
              items: result
            });
          },
          (error) => {
            this.setState({
              isLoaded: true,
              error
            });
          }
        )
    }

    areAllItemDaysEntered(selectedItems, selectedIdsWithDays){
        if (selectedIdsWithDays.map(x=>x.numberOfDays).filter(x=> isNaN(parseInt(x)) || parseInt(x)<=0).length>0)
            return false;
        if (selectedItems.filter(x=> !selectedIdsWithDays.map(z=>z.id).includes(x.toString())).length > 0)
            return false;
        return true;
    }

    submitRentalRequest = () => {
        const { selectedItems, selectedDays } = this.state;
        if (selectedItems.length===0)
            this.setState({displayAlert:true, alertText:"At least one item should be selected"});
        else{
            var selectedIdsWithDays = selectedDays.filter(x=> selectedItems.includes(parseInt(x.id)));
            if (!this.areAllItemDaysEntered(selectedItems,selectedIdsWithDays)){
                this.setState({displayAlert:true, alertText:"Positive number of days should be entered for all selected items"});
            } else{
                this.setState({displayAlert:false, alertText:null});
                const requestOptions = {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify( selectedIdsWithDays.map(x=>({ itemId:x.id, numberOfDays:x.numberOfDays })) )
                };
                fetch(this.getUrl(config.rentalEndpoint), requestOptions)
                    .then(response => response.json())
                    .then(data => this.setState({lastOrderId: data, openDialog:true}));
            }
        }
    }

    generateInvoice = () => {
        fetch(this.getUrl(`${config.invoiceEndpoint}/${this.state.lastOrderId}`))
        .then(res => res.json())
        .then(
          (result) => {
             GeneratePdf(result, this.state.lastOrderId);
          },
          (error) => {
            this.setState({
              isLoaded: true,
              error
            });
          }
        )
    }
  
    render() {
      const { error, isLoaded, items, selectedItems, selectedDays, openDialog, lastOrderId, displayAlert, alertText } = this.state;
      if (error) {
        return <div>Error: {error.message}</div>;
      } else if (!isLoaded) {
        return <div>Loading...</div>;
      } else {
        return (
            <div>
                <div style={{ display:"flex", justifyContent:"center" }}>
                    <DataTable 
                        items={items} 
                        selectedItems={selectedItems} 
                        onSelectionChange={(newSelection)=>  {
                            this.setState({selectedItems: newSelection});
                        }} 
                        onNumberOfDaysChange={(rowId, numberOfDays)=> {  
                            var row = selectedDays.find(x=>x.id.toString() === rowId);
                            if (row !== undefined){
                                row.numberOfDays = numberOfDays;
                                this.setState({selectedDays: selectedDays})        
                            }
                            else{
                                this.setState({selectedDays: [...selectedDays, {id:rowId, numberOfDays:numberOfDays }]})
                            } 
                            } }>
                    </DataTable>
                </div>
                <div style={{display:"flex", justifyContent:"center"}}>
                {displayAlert && <Alert severity="error">
                    <strong>{alertText}</strong>
                </Alert>}         
                </div>
                <Button variant="contained" onClick={this.submitRentalRequest} >Submit rental request</Button>
                <FormDialog open={openDialog} lastOrderId={lastOrderId} onGenerateInvoice={this.generateInvoice} onClose={()=>this.setState({openDialog:false})}/>
            </div>
        );
      }
    }
  }