namespace Laserfiche.Spark.Extensions.Option
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;

    /// <summary>
	/// The option type; explicitly represents nothing-or-thing nature of a value. 
	/// Supports some of the LINQ operators, such as SelectMany, Where and can be used 
	/// with linq syntax: 
	/// </summary>
	/// <example>
	/// // gets sum of the first and last elements, if they are present, orelse «-5»; 
	/// 
	/// Maybe&lt;int&gt; maybeA = list.FirstMaybe();
	/// Maybe&lt;int&gt; maybeB = list.LastMaybe();
	/// int result = (
	///		from a in maybeA
	///		from b in maybeB
	///		select a + b
	/// ).OrElse(-5);
	/// 
	/// // or shorter:
	/// var result = (from a in list.FirstMaybe() from b in list.LastMaybe() select a + b).OrElse(-5);
	/// </example>
	/// <typeparam name="T"></typeparam>
	[DataContract]
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        /// <summary>
        /// Nothing value.
        /// </summary>
        public static readonly Maybe<T> Nothing = new Maybe<T>();

        /// <summary>
        /// The value, stored in the monad. Can be accessed only if is really present, otherwise throws
        /// </summary>
        /// <exception cref="InvalidOperationException"> is thrown if not value is present</exception>
        public T Value
        {
            get
            {
                if (!this.HasValue) throw new InvalidOperationException("value is not present");
                return this._value;
            }
        }

        /// <summary>
        /// The flag of value presence
        /// </summary>
        public bool HasValue { get { return this._hasValue; } }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!this.HasValue)
            {
                return "<Nothing>";
            }

            return this.Value.ToString();
        }

        /// <summary>
        /// Automatical flattening of the monad-in-monad
        /// </summary>
        /// <param name="doubleMaybe"></param>
        /// <returns></returns>
        public static implicit operator Maybe<T>(Maybe<Maybe<T>> doubleMaybe)
        {
            return doubleMaybe.HasValue ? doubleMaybe.Value : Nothing;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        internal Maybe(T value)
        {
            Contract.Requires(value != null);

            this._value = value;
            this._hasValue = true;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Maybe<T> other)
        {
            return EqualityComparer<T>.Default.Equals(this._value, other._value) && this._hasValue.Equals(other._hasValue);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Maybe<T> && this.Equals((Maybe<T>)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(this._value) * 397) ^ this._hasValue.GetHashCode();
            }
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !left.Equals(right);
        }

        /// <summary>The _value.</summary>
        private readonly T _value;

        /// <summary>The _has value.</summary>
        private readonly bool _hasValue;
    }
}