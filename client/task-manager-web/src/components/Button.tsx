import React from "react";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
  variant?: "primary" | "secondary" | "danger";
}

const variantClasses = {
  primary: "bg-blue-600 text-white hover:bg-blue-700",
  secondary: "bg-gray-200 text-gray-900 hover:bg-gray-300",
  danger: "bg-red-600 text-white hover:bg-red-700",
};

export const Button: React.FC<ButtonProps> = ({
  children,
  variant = "primary",
  ...props
}) => (
  <button
    className={`px-4 py-2 rounded font-semibold transition-colors ${variantClasses[variant]}`}
    {...props}
  >
    {children}
  </button>
);
