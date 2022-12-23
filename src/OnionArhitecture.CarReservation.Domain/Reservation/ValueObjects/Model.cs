using System;
using System.Collections.Generic;
using System.Text;


namespace OnionArhitecture.CarReservation.Domain.Tasks.ValueObjects
{
    public readonly struct Model
    {
        private readonly string _text;

        public Model(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException($"The Model is required");
            }

            _text = text;
        }
        public override string ToString()
        {
            return _text;
        }
    }
}
