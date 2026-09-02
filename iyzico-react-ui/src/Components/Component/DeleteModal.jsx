import { motion } from "framer-motion";

export default function DeleteModal({ handleDeleteId, setshowModel }) {

    return (
        <>
            <motion.div initial={{ opacity: 0, translateY: -30 }} animate={{ opacity: 1, translateY:5 }}  className="modal d-block" tabIndex="-1">
                <div className="modal-dialog modal-dialog-centered">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">Silme Onayı</h5>
                            <button
                                type="button"
                                className="btn-close"
                                onClick={setshowModel}
                            ></button>
                        </div>

                        <div className="modal-body">
                            <p>Bu işlemi yapmak istediğine emin misin?</p>
                        </div>

                        <div className="modal-footer">
                            <button
                                type="button"
                                className="btn btn-secondary"
                                onClick={setshowModel}>İptal
                            </button>

                            <button
                                type="button"
                                className="btn btn-danger"
                                onClick={handleDeleteId}>
                                Sil
                            </button>
                        </div>
                    </div>
                </div>
            </motion.div>
            <div className="modal-backdrop fade show"></div>
        </>
    )

}