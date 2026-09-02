import { CiLogout } from "react-icons/ci";


export default function AdminHeader() {
    return (
        <>
            <div className="header">
                <div className="header-button ">
                    <button type="button" className="btn btn-danger"><CiLogout style={{fontSize: '18px', fontWeight: 'bold'}} />
                        <p className="m-0 p-2">Çıkış</p> 
                        </button>
                </div>
            </div>

        </>
    )


}