import { Snackbar } from '@mui/material';
import Alert from '@mui/material/Alert';

export default function CostumSnackbar({open, snackbarClose, severity, message }) {


    return (
        <>
            <div className="component">
                <Snackbar open={open} onClose={snackbarClose}>
                    <Alert severity={severity} onClose={snackbarClose}>
                        {message}
                    </Alert>
                </Snackbar>
            </div>
        </>

    )
}