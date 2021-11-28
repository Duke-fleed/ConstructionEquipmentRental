import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

export default function FormDialog(props) {
  return (
    <div>
      <Dialog open={props.open} onClose={props.onClose} >
        <DialogTitle>Success!</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Your rental order has been made! You can generate invoice by clicking the button below.
          </DialogContentText>
          <div style={{ display:"flex", justifyContent:"center", marginTop:30}}>
            <Button style={{ marginRight:5 }} variant="contained" onClick={props.onGenerateInvoice}>Generate Invoice</Button>
            <Button variant="contained" onClick={props.onClose}>Make another order</Button>
          </div>
        </DialogContent>
      </Dialog>
    </div>
  );
}