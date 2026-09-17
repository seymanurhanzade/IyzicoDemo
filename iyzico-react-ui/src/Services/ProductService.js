import axios from "axios";

export const getProducts = async () => {
    const res = await axios.get("/api/Product/get-all-product");
    return res;
}
export const getProductById = async (productId) => {
    try {
        const res = await axios.get(`/api/Product/${productId}`);
        console.log("Ürün detayları başarıyla alındı:", res.data);
        return res;
    } catch (error) {
        console.error("Sayfa yüklenirken bir hata oluştu:", error);
    }
    
}
export const addProduct = async (formData) => {
    try {
        const sendData = new FormData();
        sendData.append("Name", formData.Name);
        sendData.append("Price", formData.Price);
        sendData.append("Explanation", formData.Explanation);
        sendData.append("Quantity", formData.Quantity);
        sendData.append("CategoryId", formData.CategoryId);

        if (formData.ImageFile) {
            formData.ImageFile.forEach((file) => {
                sendData.append("ImageFile", file);
            });
        }
        const res = await axios.post("/api/Product/create-product", sendData, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });
        return res;
    } catch (error) {
        console.error("Ürün eklenirken bir hata oluştu:", error);
    }
}

export const deleteProduct = async (productId) => {
    try {
        const res = await axios.delete(`/api/Product/${productId}`);
        return res;
    } catch (error) {
        console.error("Ürün silinirken bir hata oluştu:", error);
    }

}

export const updateProduct = async (productId, formData) => {

    const sendData = new FormData();
    sendData.append("Name", formData.Name);
    sendData.append("Price", formData.Price);
    sendData.append("Explanation", formData.Explanation);
    sendData.append("Quantity", formData.Quantity);
    sendData.append("CategoryId", formData.CategoryId);

    if (formData.ImageFile && formData.ImageFile.length > 0) {
        formData.ImageFile.forEach((file) => {
            sendData.append("ImageFile", file);
        });
    }
    try {
        const res = axios.put(`/api/Product/${productId}`, sendData);
        return res;
    } catch (error) {
        console.error("Ürün güncellenirken bir hata oluştu:", error);
    }
}