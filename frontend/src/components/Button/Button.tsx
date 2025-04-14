import React from 'react';

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
  variant?: 'primary' | 'secondary';
}

export const Button: React.FC<ButtonProps> = ({
  children,
  onClick,
  variant = 'primary',
  disabled = false,
  type = 'button',
  ...props
}) => {
  return (
    <button
      onClick={onClick}
      disabled={disabled}
      type={type}
      className={`button ${variant} ${disabled ? 'disabled' : ''}`}
      style={{
        padding: '8px 16px',
        borderRadius: '4px',
        border: 'none',
        cursor: disabled ? 'not-allowed' : 'pointer',
        backgroundColor: variant === 'primary' ? '#007bff' : '#6c757d',
        color: 'white',
        opacity: disabled ? 0.6 : 1,
      }}
      {...props}
    >
      {children}
    </button>
  );
}; 