import axios from "axios";


export const GetOrders = async () => {
    try {
        const response = await axios.get('/api/Order');
        return response.data;
    } catch (error) {
        console.error("Error fetching orders:", error);
    }
}