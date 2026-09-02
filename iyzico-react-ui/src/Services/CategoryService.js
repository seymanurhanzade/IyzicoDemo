import axios from "axios";

export const getCategories = async () => {
    return axios.get("/api/Product/get-all-category");
};

export const addCategory = async (categoryNameData) => {
    try {
        const ctgData = new FormData();
        ctgData.append("CategoryName", categoryNameData.CategoryName);
        const response = await axios.post("/api/Product/add-category", ctgData, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        });
        return response.data;
    } catch (error) {
        console.error("Error adding category:", error);
    }

}

export const updateCategory = async (id, categoryData) => {
    try {
        const data = new FormData();
        data.append("CategoryName", categoryData.CategoryName)
        const res = await axios.put(`http://localhost:5174/api/Product/update-category/${id}`, data);
        return res.data;
    } catch (error) {
        console.error("Service Hatası:", error.response?.data || error.message);
        throw error; 
    }
}


export const deleteCategory = async (categoryId) => {
    try {
        const response = await axios.delete(`/api/Product/category-delete-${categoryId}`);
        return response.data;
    } catch (error) {
        console.error("Error deleting category:", error);
    }
}