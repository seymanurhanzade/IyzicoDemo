import axios from "axios";

export const Register = async (data) => {
    try {
        const sendData = new FormData();
        sendData.append("FullName", data.FullName);
        sendData.append("Email", data.Email);
        sendData.append("Password", data.Password);
        const res = await axios.post("api/Account/register", sendData);
        return res;
    } catch (error) {
        console.log("hata: ", error);
    }
}
export const Login = async (data) => {
    try {
        const sendData = new FormData();
        sendData.append("Email", data.Email);
        sendData.append("Password", data.Password);
        const res = await axios.post("api/Account/login", sendData);
        return res;
    } catch (error) {
        console.log("hata: ", error);
    }
}
export const Logout = async () => {
    try {
        const res = await axios.post("api/Account/logout");
        return res;
    } catch (error) {
        console.log("hata: ", error);
    }
}