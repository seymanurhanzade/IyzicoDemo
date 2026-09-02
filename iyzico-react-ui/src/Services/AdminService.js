import axios from "axios"

export const GetCounts = async () => {
    try {
        const rest = await axios.get("/api/Account/management-counts");
        console.log("Service baþarýlý.");
        return rest;
    } catch (error) {
        console.log("Service hata. ", error);
    }
    
}