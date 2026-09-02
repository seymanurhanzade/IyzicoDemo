import { useState } from "react";
import { Register, Login, Logout } from "../../Services/AccountService";
import { useNavigate } from "react-router-dom";

export default function LoginPage() {
    const [datas, setDatas] = useState({ Email: "", Password: "" });
    const navigate = useNavigate();
    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setDatas((prev) => ({
            ...prev,
            [name]: value,
        }))
    }

    const handleLogin = async () => {
        try {
            const res = await Login(datas);
            navigate("/home")
            console.log("giriş başarılı: ", res);
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
                                    <input type="email" className="form-control" id="floatingInput" placeholder="name@example.com" name="Email" value={datas.Email} onChange={handleInputChange} />
                                    <label htmlFor="floatingInput">Email</label>
                                </div>
                                <div className="form-floating">
                                    <input type="password" className="form-control" id="floatingPassword" placeholder="Password" name="Password" value={datas.Password} onChange={handleInputChange} />
                                    <label htmlFor="floatingPassword">Şifre</label>
                                </div>
                                <button type="button" className="btn btn-primary" onClick={handleLogin}>Giriş Yap</button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>

    )

}