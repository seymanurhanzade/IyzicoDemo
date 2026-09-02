import { useState } from "react";
import { Register, Login, Logout } from "../../Services/AccountService";
import { useNavigate } from "react-router-dom"; 
export default function RegisterPage() {
    const [datas, setDatas] = useState({ FullName: "", Email: "", Password: "" });
    const navigate = useNavigate();
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setDatas((prev) => ({
            ...prev,
            [name]: value,
        }))
    }

    const handleRegister = async () => {
        try {
            const res = await Register(datas);
            navigate("/login");
            console.log("kayıt başarılı: ", res);
        } catch (error) {
            console.log("hata: ", error);
        }
    }
    return (
        <>
            <div className="component">

                <div className="login-page">
                    <div className="login-container">
                        <div className="card" style={{ width: '30rem' }}>
                            <div className="card-body m-auto">
                                <h5 className="card-title">Giriş</h5>
                                <hr></hr>
                                <div className="form-floating mb-3">
                                    <input type="text" className="form-control" id="floatingFullName" placeholder="Full Name" name="FullName" value={datas.FullName} onChange={handleInputChange} />
                                    <label htmlFor="floatingFullName">Full Name</label>
                                </div>
                                 <div className="form-floating mb-3">
                                    <input type="email" className="form-control" id="floatingInput" placeholder="name@example.com" name="Email" value={datas.Email} onChange={handleInputChange} />
                                    <label htmlFor="floatingInput">Email</label>
                                </div>
                                <div className="form-floating">
                                    <input type="password" className="form-control" id="floatingPassword" placeholder="Password" name="Password" value={datas.Password} onChange={handleInputChange} />
                                    <label htmlFor="floatingPassword">Şifre</label>
                                </div>
                                <button type="button" className="btn btn-primary" onClick={handleRegister}>Kayıt Ol</button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>

    )

}