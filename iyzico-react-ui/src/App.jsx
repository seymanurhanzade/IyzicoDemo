import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import './css/style.css'
import Menu from './Components/Admin/Menu'
import AppRoutes from './routes/AppRoutes'
import AdminHeader from './Components/Admin/AdminHeader'

function App() {
    return (
        <>
            <div>
                {/*<AdminHeader />*/}
                <div className="row w-100 m-0">
                    <div className="col-md-2 p-0">
                        <Menu />
                    </div>
                    <div className="col-md-10 p-0">
                        <AdminHeader />
                        <AppRoutes />
                    </div>
                </div>
            </div>

        </>
    )
}

export default App