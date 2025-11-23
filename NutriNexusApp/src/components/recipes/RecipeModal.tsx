import React, { useState, FormEvent } from 'react';
//import { Recipe } from '../../models/types';
import styles from './RecipeModal.module.css';
import { RiCloseLine } from 'react-icons/ri';

interface ModalProps {
  onClose: () => void;
}

export default function RecipeModal({ onClose }: ModalProps) {
  return (
    <>
      <div className={styles.darkBG}>
        <div className={styles.centered}>
          <div className={styles.modal}>
            <div className={styles.modalHeader}>
              <h5 className={styles.heading}>Add New Recipe</h5>
            </div>
            <div>
              <button className={styles.closeBtn} onClick={() => onClose()}>
                <RiCloseLine style={{ marginBottom: '-3px' }} />
              </button>
            </div>
            <div className={styles.modalContent}>
              Are you sure you want to delete the item?
            </div>
            <div className={styles.modalActions}>
              <div className={styles.actionsContainer}>
                <button className={styles.deleteBtn} onClick={() => onClose()}>
                  Delete
                </button>
                <button className={styles.cancelBtn} onClick={() => onClose()}>
                  Cancel
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
